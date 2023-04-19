using Microsoft.AspNetCore.Mvc;
using store_vegetable.Services.StoreVegetable;
using System.Net;
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
            return app;
        }
        private async static Task<IResult> Login([AsParameters] LoginModel model,[FromServices]IUserRepository userRepository , [FromServices] IUserTokenRepository userTokenRepository, [FromServices] IJwtTokenRepository jwtTokenRepository)
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
    }
}
