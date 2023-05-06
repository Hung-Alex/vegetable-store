using FluentValidation;
using Mapster;
using MapsterMapper;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using store_vegetable.Core.Collections;
using store_vegetable.Core.DTO;
using store_vegetable.Core.Entites;
using store_vegetable.Data.Extensions;
using store_vegetable.Services.Media;
using store_vegetable.Services.StoreVegetable;
using System.Net;
using System.Security.Claims;
using WebApi.Models;

namespace WebApi.Endpoints
{
    public static class FoodEnpoints
    {
        public static WebApplication MapFoodEndpoints(this WebApplication app)
        {
            var routeGroupBuilder = app.MapGroup("/api/Foods");

            routeGroupBuilder.MapGet("/", GetFoods)
                 .WithName("GetFoods")
                 .Produces<ApiResponse<PaginationResult<FoodDto>>>();
                 
                 
                 

            routeGroupBuilder.MapGet("/BestSellingFood/{limit:int}", BestSellingFood)
                 .WithName("BestSellingFood")
                 .Produces<ApiResponse<List<FoodDto>>>();

            routeGroupBuilder.MapGet("/{id:int}", GetFoodById)
                 .WithName("GetFoodById")
                 .Produces<ApiResponse<FoodDto>>();


            routeGroupBuilder.MapDelete("/{id:int}", DeleteFood)
                           .WithName("DeleteFood")
                           .Produces(204)
                           .Produces(404)
                           .RequireAuthorization("Admin");

            routeGroupBuilder.MapPost("/", AddFood)
                .WithName("AddFood")
                .Accepts<FoodEditModel>("multipart/form-data")
                .Produces(401)
                .Produces<ApiResponse<FoodDto>>()
                .RequireAuthorization("Admin");
            return app;
        }

        private async static Task<IResult> GetFoods([AsParameters] FoodFilterModel model,[AsParameters]PagingModel paging, HttpContext context, IFoodRepository foodRepository,IMapper mapper )
        {

            var foodQuery = mapper.Map<FoodQuery>(model);
            var postsList = await foodRepository.GetFoodsByQuery(foodQuery, paging, foods => foods.ProjectToType<FoodDto>());

            var paginationResult = new PaginationResult<FoodDto>(postsList);
             

            return Results.Ok(ApiResponse.Success(paginationResult));
            
        }
        private static async Task<IResult> GetFoodById(int id,IFoodRepository foodRepository,IMapper mapper)
        {
            var food = await foodRepository.GetFoodById(id);

            return Results.Ok(ApiResponse.Success( mapper.Map<FoodDto>(food)));
        }
        private static async Task<IResult> AddFood(HttpContext context
           , IFoodRepository foodRepository
           , IMediaManager mediaManager
           , IMapper mapper
           , IValidator<FoodEditModel> validator)

        {
            var model = await FoodEditModel.BindAsync(context);

            // check model is valid
            var validationResult = await validator.ValidateAsync(model);
            if (!validationResult.IsValid)
            {
                return Results.Ok(ApiResponse.Fail(HttpStatusCode.BadRequest, validationResult));
            }

            var slug = model.Name.GenerateSlug();

            if (await foodRepository.IsSlugFoodExisted(0, slug))
            {
                return Results.Ok(ApiResponse.Fail(HttpStatusCode.Conflict, $"Slug '{slug}' đã được sử dụng cho chủ đề khác"));
            }
            var food = model.Id > 0 ? await foodRepository.GetFoodById(model.Id) : null;
            if (food == null)
            {
                food = mapper.Map<Food>(model);
                if (await foodRepository.IsSlugFoodExisted(0, slug))
                {
                    return Results.Ok(ApiResponse.Fail(HttpStatusCode.Conflict, $"Slug '{slug}' đã được sử dụng cho chủ đề khác"));
                }

            }
            if(model.Id>0)
            {
                if (await foodRepository.IsSlugFoodExisted(model.Id, slug))
                {
                    return Results.Ok(ApiResponse.Fail(HttpStatusCode.Conflict, $"Slug '{slug}' đã được sử dụng cho chủ đề khác"));
                }
            }
            food.UrlSlug = slug;
            food.Name = model.Name;
            food.Description = model.Description;
            food.ShowOnPage = model.ShowOnPage;
            food.Unit=model.Unit;
            food.Weight = model.Weight;
            food.Price=model.Price;
            food.CategoriesId=model.CategoriesId;


            if (model.ImageFile?.Length > 0)
            {
                string hostname = $"{context.Request.Scheme}://{context.Request.Host}{context.Request.PathBase}/";
                string uploadedPath = await mediaManager.SaveFileAsync(model.ImageFile.OpenReadStream(), model.ImageFile.FileName, model.ImageFile.ContentType);
                if (!string.IsNullOrWhiteSpace(uploadedPath))
                {
                    food.Image = uploadedPath;
                }
            }
            await foodRepository.AddOrUpdateFood(food);

            return Results.Ok(ApiResponse.Success(mapper.Map<FoodDto>(food), HttpStatusCode.Created));
        }


        private static async Task<IResult> BestSellingFood(int limit, IFoodRepository foodRepository,IMapper mapper)
        {
            var foods = await foodRepository.GetBestSellingFood(limit);
            
            return Results.Ok( ApiResponse.Success(foods.Select(x=>mapper.Map<FoodDto>(x)).ToList().Take(limit)) );
        }

        private static async Task<IResult> DeleteFood(int id, IFoodRepository foodRepository)
        {
            var status = await foodRepository.DeleteFood(id);
            return Results.Ok(status ? ApiResponse.Success(HttpStatusCode.NoContent) : ApiResponse.Fail(HttpStatusCode.NotFound, $"không tìm thấy food với mã {id}"));
        }
    }
}
