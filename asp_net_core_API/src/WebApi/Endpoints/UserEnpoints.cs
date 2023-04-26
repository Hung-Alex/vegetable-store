using Azure.Core;
using FluentValidation;
using Mapster;
using MapsterMapper;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore.Diagnostics;
using store_vegetable.Core.Collections;
using store_vegetable.Core.DTO;
using store_vegetable.Core.Entites;
using store_vegetable.Data.Extensions;
using store_vegetable.Services.StoreVegetable;
using System.Net;
using WebApi.Extensions;
using WebApi.Filters;
using WebApi.Models;
using WebApi.Validations;

namespace WebApi.Endpoints
{
    public static class UserEnpoints
    {
        public static WebApplication MapUserEndpoints( this WebApplication app)
        {
            var groupRouteBuilder = app.MapGroup("/api/Users");
            groupRouteBuilder.MapGet("/", GetPagedUsers)
                .WithName("GetPagedUsers")
                .Produces<ApiResponse<PaginationResult<UserDto>>>()
                .Produces(401)
                .RequireAuthorization("Admin");

            groupRouteBuilder.MapGet("/{id:int}", GetUserById)
               .WithName("GetUserById")
               .Produces<ApiResponse<UserDto>>()
               .Produces(401)
               .RequireAuthorization("Admin");

            groupRouteBuilder.MapPut("/{id:int}", UpdateUser)
               .WithName("UpdateUser")
               .Produces<ApiResponse>()
               .Accepts<UserEditModel>("multipart/form-data")
               .Produces(401)
               .RequireAuthorization("Admin");
            groupRouteBuilder.MapDelete("/{id:int}", DeleteUser)
               .WithName("DeleteUser")
               .Produces<ApiResponse>()         
               .Produces(401)
               .RequireAuthorization("Admin");
            return app;
        }
        private async static Task<IResult> GetPagedUsers([AsParameters] UserFilterModel model,[AsParameters] PagingModel pagingModel,IUserRepository userRepository,HttpContext context,IMapper mapper) 
        {
            var userQuery=mapper.Map<UserQuery>(model);
            var users = await userRepository.GetPagedUserList(userQuery, pagingModel, user => user.ProjectToType<UserDto>());
            var paginationResult = new PaginationResult<UserDto>(users);
            return Results.Ok(ApiResponse.Success(paginationResult));
        }
        private async static  Task<IResult> GetUserById(int id, [FromServices] IUserRepository userRepository ,IMapper mapper)
        {
            var user = await userRepository.GetById(id);

            if (user == null) return Results.Ok(ApiResponse.Fail(HttpStatusCode.NotFound,$"Khong tim thay người dùng với mã  {id}"));
            user.Password = user.Password.DecodeFrom64();

            return Results.Ok(ApiResponse.Success(mapper.Map<UserDto>(user)));
        }
        private async static Task<IResult> UpdateUser(int id,[FromServices] IUserRepository userRepository,IMapper mapper,HttpContext context,[FromServices]IValidator<UserEditModel> validator)
        {
            var userEditModel = await UserEditModel.BindAsync(context);
            var validatorResult = await validator.ValidateAsync(userEditModel);
            if (!validatorResult.IsValid)
            {
                return Results.Ok(ApiResponse.Fail(HttpStatusCode.BadRequest, validatorResult));
            }

            var user= await userRepository.GetById(id);
            if (user==null)
            {
                return Results.Ok(ApiResponse.Fail(HttpStatusCode.NotFound, $"Khong tim thay người dùng với mã  {id}"));
            }
            user.Name=userEditModel.Name;
            user.Password= userEditModel.Password.EncodePasswordToBase64();
            user.Role= userEditModel.Role;
           var checkUpdate= await userRepository.UpdateUser(user);

            return checkUpdate? Results.Ok(ApiResponse.Success(HttpStatusCode.NoContent)): Results.Ok(ApiResponse.Fail(HttpStatusCode.NotFound, $"cập nhật thất bại  với mã người dùng{ id}"));
        }
        private async static Task<IResult> DeleteUser(int id, [FromServices] IUserRepository userRepository)
        {
            if (!await userRepository.RemoveUser(id))
            {
                return Results.Ok(ApiResponse.Fail(HttpStatusCode.BadRequest,$"không tìm thấy người dùng với mã {id}"));

            }
            return Results.Ok(ApiResponse.Success(HttpStatusCode.NoContent));
        }
        
    }
}
