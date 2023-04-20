using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Mvc;
using store_vegetable.Core.Collections;
using store_vegetable.Core.DTO;
using store_vegetable.Core.Entites;
using store_vegetable.Services.StoreVegetable;
using System.Net;
using System.Net.NetworkInformation;
using System.Security.Claims;
using WebApi.Models;

namespace WebApi.Endpoints
{
    public static class CartEnpoints
    {
        public static WebApplication MapCartEnpoints(this WebApplication app)
        {
            var routeGroupBuilder = app.MapGroup("/api/Cart");
            routeGroupBuilder.MapGet("/", GetAllItemInCart)
                 .WithName("GetAllItemInCart")
                 .Produces<ApiResponse<PaginationResult<CartItemDto>>>()
                 .RequireAuthorization("User");

            routeGroupBuilder.MapPost("/", AddItemInCart)
                .WithName("AddItemInCart")
                .Produces<ApiResponse>()
                .Accepts<CartItemEditModel>("multipart/form-data")
                .RequireAuthorization("User");

            routeGroupBuilder.MapPut("/", UpdateItemInCart)
               .WithName("UpdateItemInCart")
               .Produces<ApiResponse<PaginationResult<CartItemDto>>>()
               .RequireAuthorization("User");
            return app;
        }
        private static async Task<IResult> GetAllItemInCart([AsParameters] PagingModel paging,HttpContext context,[FromServices]ICartRepository cartRepository)
        {
            var authenticateResult = await context.AuthenticateAsync("User");
            if (authenticateResult?.Succeeded == true)
            {
                int userId = int.Parse(authenticateResult.Principal.FindFirstValue("Id"));
                var cart = await cartRepository.GetCartByUserIdAsync(userId);
                var cartItemList=await cartRepository.GetAllItemInCart(cart.Id,paging);
                var paginationResult = new PaginationResult<CartItemDto>(cartItemList);

                return Results.Ok(ApiResponse.Success(paginationResult));

            }

            var failureReason = authenticateResult?.Failure?.Message ?? "Unknown reason";
            return Results.Ok(ApiResponse.Fail(HttpStatusCode.BadRequest, failureReason));
        }
        private static async Task<IResult> AddItemInCart(HttpContext context, [FromServices] ICartRepository cartRepository)
        {
            var cartitemEdit = await CartItemEditModel.BindAsync(context);
            var authenticateResult = await context.AuthenticateAsync("User");
            if (authenticateResult?.Succeeded == true)
            {
                int userId = int.Parse(authenticateResult.Principal.FindFirstValue("Id"));
                var cart = await cartRepository.GetCartByUserIdAsync(userId);
                var cartItem= await cartRepository.ItemIsExitedInCart(cartitemEdit.Id, cart.Id);
                if (cartItem!=null)
                {
                    cartItem.Quantity = cartItem.Quantity+cartitemEdit.Quantity;
                    await cartRepository.UpdateCartItem(cartItem);
                    return Results.Ok(ApiResponse.Success(HttpStatusCode.NoContent));

                }
                else
                {
                    await cartRepository.AddItemInCartAsync(new CartItem() { 
                        CartId=cart.Id,
                        Quantity = cartitemEdit.Quantity,
                        FoodId= cartitemEdit.Id

                    });
                    return Results.Ok(ApiResponse.Success(HttpStatusCode.NoContent));

                }
            }

            var failureReason = authenticateResult?.Failure?.Message ?? "Unknown reason";
            return Results.Ok(ApiResponse.Fail(HttpStatusCode.BadRequest, failureReason));
        }

        private static async Task<IResult> UpdateItemInCart(CartItemEditModel model,[FromServices]ICartRepository cartRepository,HttpContext context)
        {
            
            var authenticateResult = await context.AuthenticateAsync("User");
            if (authenticateResult?.Succeeded == true)
            {
                int userId = int.Parse(authenticateResult.Principal.FindFirstValue("Id"));
                var cart = await cartRepository.GetCartByUserIdAsync(userId);
                var cartItem = await cartRepository.ItemIsExitedInCart(model.Id, cart.Id);
                if (cartItem != null)
                {
                    cartItem.Quantity = cartItem.Quantity + model.Quantity;
                    await cartRepository.UpdateCartItem(cartItem);
                    return Results.Ok(ApiResponse.Success(HttpStatusCode.NoContent));

                }
                else
                {
                    await cartRepository.AddItemInCartAsync(new CartItem()
                    {
                        CartId = cart.Id,
                        Quantity = model.Quantity,
                        FoodId = model.Id

                    });
                    return Results.Ok(ApiResponse.Success(HttpStatusCode.NoContent));

                }
            }

            var failureReason = authenticateResult?.Failure?.Message ?? "Unknown reason";
            return Results.Ok(ApiResponse.Fail(HttpStatusCode.BadRequest, failureReason));
            return Results.Ok();
        }
    }
}
