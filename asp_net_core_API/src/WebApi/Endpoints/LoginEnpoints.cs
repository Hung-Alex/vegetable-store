using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Mvc;
using store_vegetable.Services.StoreVegetable;
using System.Net;
using System.Security.Claims;
using WebApi.JwtToken;
using WebApi.Models;


namespace WebApi.Endpoints
{
    public static class LoginEnpoints
    {
        public static WebApplication MapLoginEndpoints(this WebApplication app)
        {
            var routeGroupBuilder = app.MapGroup("/api/Account");
            routeGroupBuilder.MapPost("/Login", Login)
                            .WithName("Login")
                            .Produces(404)
                            .Produces<ApiResponse<UserTokenModel>>();
            routeGroupBuilder.MapPost("/Logout", Logout)
                            .WithName("Logout")
                            .Produces(404)
                            .Produces<ApiResponse>()
                            .RequireAuthorization("Logout");
            routeGroupBuilder.MapPost("/Register", Register)
                            .WithName("Register")
                            .Produces(404)
                            .Produces<ApiResponse>();
                            
            return app;
        }
        private async static Task<IResult> Login( LoginModel model,[FromServices]IUserRepository userRepository , [FromServices] IUserTokenRepository userTokenRepository, [FromServices] IJwtTokenRepository jwtTokenRepository)
        {
            var user = await userRepository.Login(model.UserName, model.Password);
            if (user == null)
            {
                return Results.Ok(ApiResponse.Fail(HttpStatusCode.NotFound,"khong tim thay tai khoan nhu vay"));
            }
            var token = await jwtTokenRepository.GenerateJwtToken(user);
            await userTokenRepository.AddOrUpdateUserToken(user.Id,token);
            return Results.Ok(ApiResponse.Success(new UserTokenModel()
            {
                Id = user.Id,
                Role = user.Role,
                Token=token,
                Expired = DateTime.UtcNow.AddDays(4)


            })) ;
        }
        private static async Task<IResult> Logout(HttpContext context, IUserRepository userRepository, IUserTokenRepository userTokenRepository)
        {
            var authenticateResult = await context.AuthenticateAsync("User");// sai logic >-<
            if (authenticateResult?.Succeeded == true)
            {
                int userId = int.Parse(authenticateResult.Principal.FindFirstValue("Id"));
                string userRole = authenticateResult.Principal.FindFirstValue("Role");
                await userTokenRepository.AddOrUpdateUserToken(userId, null);
                await userTokenRepository.SetStatusAccount(userId, false);

                return Results.Ok(ApiResponse.Success(HttpStatusCode.NoContent));

            }

            var failureReason = authenticateResult?.Failure?.Message ?? "Unknown reason";


            return Results.Ok(ApiResponse.Fail(HttpStatusCode.BadRequest, failureReason));
        }

        private static async Task<IResult> Register(RegisterModel model,IUserRepository userRepository,ICartRepository cartRepository)
        {
            if (model!=null)
            {
                if (await userRepository.Register(model.UserName,model.Password,model.ConfirmPassword))
                {
                    var user= await userRepository.GetUserByUserName(model.UserName);
                    await cartRepository.CreateCartAsync(user.Id);
                    return Results.Ok(ApiResponse.Success(HttpStatusCode.NoContent));
                }
                else
                {
                    return Results.Ok(ApiResponse.Fail(HttpStatusCode.BadRequest,"Tên tài khoản đã tồn tại "));
                }
            }
            return Results.Ok(ApiResponse.Fail(HttpStatusCode.BadRequest, "Ko thể tạo tài khoản"));
        }
    }
}
