using Mapster;
using MapsterMapper;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Routing;
using store_vegetable.Core.Collections;
using store_vegetable.Core.DTO;
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
            var routeGroupBuilder = app.MapGroup("/api/Orders");

            routeGroupBuilder.MapPost("/", CreateOrder)
                .WithName("CreateOrder")
                .Accepts<OrderEditModel>("multipart/form-data")
                .RequireAuthorization("User");
            routeGroupBuilder.MapGet("/", GetPagedOrderList)
                .WithName("GetPagedOrderList")
                .Produces<ApiResponse<PaginationResult<Order>>>()
                .RequireAuthorization("Admin");
            routeGroupBuilder.MapPut("/{id:int}", SetStatusForOrder)
                .WithName("SetStatusForOrder")
                .Produces<ApiResponse>()
                .RequireAuthorization("Admin");
            routeGroupBuilder.MapDelete("/{id:int}", DeleteOrder)
                           .WithName("DeleteOrder")
                           .Produces(204)
                           .Produces(404)
                           .RequireAuthorization("Admin");

            return app;
        }
        private static async Task<IResult> SetStatusForOrder(int id, [FromServices] IOrderRepository orderRepository, HttpContext context)
        {

            var authenticateResult = await context.AuthenticateAsync("Admin");
            if (authenticateResult?.Succeeded == true)
            {
                int userId = int.Parse(authenticateResult.Principal.FindFirstValue("Id"));

                var changeStatus = await orderRepository.SetOrderStatus(id);

                if (!changeStatus)
                {
                    return Results.Ok(ApiResponse.Fail(HttpStatusCode.BadRequest, $"Không có đơn hàng với id là {id}"));
                }


                return Results.Ok(ApiResponse.Success(HttpStatusCode.NoContent));

            }
            var failureReason = authenticateResult?.Failure?.Message ?? "Unknown reason";
            return Results.Ok(ApiResponse.Fail(HttpStatusCode.BadRequest, failureReason));
        }
        private async static Task<IResult> GetPagedOrderList([AsParameters] OrderFilterDto orderFilterDto, [AsParameters] PagingModel pagingModel, [FromServices] IOrderRepository orderRepository, IMapper mapper)
        {
            var orderQuery = mapper.Map<OrderQuery>(orderFilterDto);
            var orderList = await orderRepository.GetPagedListOrder(orderQuery, pagingModel, orders => orders.ProjectToType<OrderDto>());
            var paginationResult = new PaginationResult<OrderDto>(orderList);
            return Results.Ok(ApiResponse.Success(paginationResult));
        }
        private async static Task<IResult> CreateOrder([FromServices] ICartRepository cartRepository, IOrderRepository orderRepository, HttpContext context, IMapper mapper)
        {

            var model = await OrderEditModel.BindAsync(context);
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
                await orderRepository.CreateOrder(userId, cart.Id, mapper.Map<Order>(model));
                await cartRepository.RemoveAllItemInCartAsync(cart.Id);

                return Results.Ok(ApiResponse.Success(HttpStatusCode.NoContent));

            }
            var failureReason = authenticateResult?.Failure?.Message ?? "Unknown reason";
            return Results.Ok(ApiResponse.Fail(HttpStatusCode.BadRequest, failureReason));

        }
        private async static Task<IResult> DeleteOrder(int id, [FromServices] IOrderRepository orderRepository)
        {
            var order = await orderRepository.GetOrderById(id);
            if (order == null)
            {
                return Results.Ok(ApiResponse.Fail(HttpStatusCode.BadRequest, $"Không có đơn hàng với id là {id}"));
            }
            if (await orderRepository.DeleteOrderById(id))
            {
                return Results.Ok(ApiResponse.Success($"Xóa thành công  đơn hàng với id là {id}",HttpStatusCode.NoContent));
            }

            return Results.Ok(ApiResponse.Fail(HttpStatusCode.Conflict, $"Xóa ko thành công  đơn hàng với id là {id}"));

        }
    }
}
