using MapsterMapper;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Mvc;
using store_vegetable.Core.Entites;
using store_vegetable.Services.StoreVegetable;
using System.Net;
using System.Security.Claims;
using WebApi.Models;

namespace WebApi.Endpoints
{
    public static class OrderEnpoints
    {
        public static WebApplication MapOrderEnpoints(this WebApplication app)
        {
            var routeGriupBuilder = app.MapGroup("/api/Orders");

            routeGriupBuilder.MapPost("/", CreateOrder)
                .WithName("CreateOrder")
                .Accepts<OrderDto>("multipart/form-data")
                .RequireAuthorization("User");

            return app;
        }
        private async static Task<IResult> CreateOrder([FromServices] ICartRepository cartRepository, IOrderRepository orderRepository, HttpContext context,IMapper mapper)
        {

            var model = await OrderDto.BindAsync(context);
            var authenticateResult = await context.AuthenticateAsync("User");
            if (authenticateResult?.Succeeded == true)
            {
                int userId = int.Parse(authenticateResult.Principal.FindFirstValue("Id"));
                var cart = await cartRepository.GetCartByUserIdAsync(userId);
                var emptyCart = await cartRepository.CartHasEmptyAsync(cart.Id);
                if (emptyCart)
                {
                    return Results.Ok(ApiResponse.Fail(HttpStatusCode.NoContent, "giỏ hàng của bạn đang rỗng"));
                }
                await orderRepository.CreateOrder(userId,cart.Id,mapper.Map<Order>(model));
                await cartRepository.RemoveAllItemInCartAsync(cart.Id);

                return Results.Ok(ApiResponse.Success(HttpStatusCode.NoContent));

            }
            var failureReason = authenticateResult?.Failure?.Message ?? "Unknown reason";
            return Results.Ok(ApiResponse.Fail(HttpStatusCode.BadRequest, failureReason));
        }
    }
}
