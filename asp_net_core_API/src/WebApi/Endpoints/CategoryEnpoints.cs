using Mapster;
using MapsterMapper;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Hosting;
using store_vegetable.Core.Collections;
using store_vegetable.Core.DTO;
using store_vegetable.Services.StoreVegetable;
using WebApi.Models;
using System.Net;
using store_vegetable.Services.Media;
using store_vegetable.Data.Mappings;
using store_vegetable.Core.Entites;
using store_vegetable.Data.Extensions;

namespace WebApi.Endpoints
{
    public static class CategoryEnpoints
    {
        public static WebApplication MapCategoryEnpoints(
          this WebApplication app)
        {
            var routeGroupBuilder = app.MapGroup("/api/categories");
            routeGroupBuilder.MapGet("/", GetCategories)
                            .WithName("GetCategories")
                            .Produces<ApiResponse<CategoryDto>>();
            routeGroupBuilder.MapGet("/{slug::regex(^[a-z0-9_-]+$)}/foods", GetFoodsByCategorySlug)
                           .WithName("GetFoodsByCategorySlug")
                           .Produces<ApiResponse<PaginationResult<FoodDto>>>();

            routeGroupBuilder.MapDelete("/{id:int}",DeleteCategory)
                            .WithName("DeleteCategory")
                            .Produces(204)
                            .Produces(404);
            routeGroupBuilder.MapPost("/",AddCategory)
                .WithName("AddCategory")
                .Accepts<CategoryEditModel>("multipart/form-data")
                .Produces(401)
                .Produces<ApiResponse<CategoryItem>>();

            return app;
        }
        private static async Task<IResult> GetCategories([FromServices] ICategoryRepository categoryRepository,IMapper mapper,HttpContext context)
        {
            var categories = await categoryRepository.GetAllCategories();
            var result= categories.Select(t => mapper.Map<CategoryDto>(t)).ToList();
            return Results.Ok(ApiResponse.Success(result));

        }
        public static async Task<IResult> GetFoodsByCategorySlug([FromRoute] string slug, [AsParameters] PagingModel model, ICategoryRepository categoryRepository)
        {
            var foodQuery = new FoodQuery
            {
                CategorySlug=slug
            };
            var foodlist =  await categoryRepository.GetFoodsByCategorySlug(foodQuery, model, foods => foods.ProjectToType<FoodDto>());
            var paginationResult = new PaginationResult<FoodDto>(foodlist);
            return Results.Ok(ApiResponse.Success( paginationResult));
        }
        private static async Task<IResult> AddCategory(HttpContext context, ICategoryRepository categoryRepository, IMediaManager mediaManager,IMapper mapper)
        {
            var model = await CategoryEditModel.BindAsync(context);
            var slug = model.Name.GenerateSlug();
            
            if (await categoryRepository.IsCategorySlugExistedAsync(0,slug))
            {
                return Results.Ok(ApiResponse.Fail(HttpStatusCode.Conflict, $"Slug '{slug}' đã được sử dụng cho chủ đề khác"));
            }
            var category = mapper.Map<Categories>(model);

            category.UrlSlug=slug;

            if (model.ImageFile?.Length > 0)
            {
                string hostname = $"{context.Request.Scheme}://{context.Request.Host}{context.Request.PathBase}/";
                string uploadedPath = await mediaManager.SaveFileAsync(model.ImageFile.OpenReadStream(), model.ImageFile.FileName, model.ImageFile.ContentType);
                if (!string.IsNullOrWhiteSpace(uploadedPath))
                {
                    category.Image = uploadedPath;
                }
            }
            await categoryRepository.AddOrUpdateCategory(category);

            return Results.Ok(ApiResponse.Success(mapper.Map<CategoryItem>(category), HttpStatusCode.Created));
        }
        private static async Task<IResult> DeleteCategory(int id,ICategoryRepository categoryRepository)
        {
            var status= await categoryRepository.DeleteCategory(id);
            return Results.Ok(status?ApiResponse.Success(HttpStatusCode.NoContent) : ApiResponse.Fail(HttpStatusCode.NotFound,$"không tìm thấy category với mã {id}"));
        }
    }
}
