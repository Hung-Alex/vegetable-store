using FluentValidation;
using Mapster;
using MapsterMapper;
using Microsoft.AspNetCore.Mvc;
using store_vegetable.Core.Collections;
using store_vegetable.Core.DTO;
using store_vegetable.Core.Entites;
using store_vegetable.Data.Extensions;
using store_vegetable.Services.Media;
using store_vegetable.Services.StoreVegetable;
using System.Net;
using WebApi.Models;

namespace WebApi.Endpoints
{
    public static class FeedbackEnpoints
    {
        public static WebApplication MapFeedbackEnpoints(this WebApplication app)
        {
            var routeGroupBuilder = app.MapGroup("/api/Feedbacks");
            routeGroupBuilder.MapGet("/", GetFeedbacks)
                            .WithName("GetFeedbacks")
                            .Produces<ApiResponse<FeedbackDto>>();

            routeGroupBuilder.MapGet("/{slug::regex(^[a-z0-9_-]+$)}/feedbacks", GetFeedbacksBySlug)
                           .WithName("GetFeedbacksBySlug")
                           .Produces<ApiResponse<PaginationResult<FeedbackDto>>>();

            routeGroupBuilder.MapDelete("/{id:int}", DeleteFeedback)
                            .WithName("DeleteFeedback")
                            .Produces(204)
                            .Produces(404);

            routeGroupBuilder.MapPost("/", AddFeeback)
                .WithName("AddFeeback")
                .Accepts<FeedbackEditModel>("multipart/form-data")
                .Produces(401)
                .Produces<ApiResponse<FeedbackDto>>();

            routeGroupBuilder.MapPost("/{id:int}/feedback", setStatusFeedback)
                             .WithName("setStatusFeedback")
                             .Produces(401)
                             .Produces<ApiResponse<FeedbackDto>>();

            return app;
        }
        private static async Task<IResult> setStatusFeedback([FromRoute] int id, [FromServices] IFeedbackRepository feedbackRepository,IMapper mapper)
        {
            var feedback = await feedbackRepository.GetFeedbackById(id);
           

            if (feedback==null) {    
                return Results.Ok(ApiResponse.Fail(HttpStatusCode.NotFound,$"khong tim thay feedback nao với với Id {id}  "));
            }
            await feedbackRepository.SetStatusFeedback(feedback);

            

            return Results.Ok(ApiResponse.Success(mapper.Map<FeedbackDto>(feedback)));
        }
        private static async Task<IResult> GetFeedbacks([FromServices] IFeedbackRepository feedbackRepository, IMapper mapper, HttpContext context)
        {
            var feedbacks = await feedbackRepository.GetAllFeedbacks();
            var result = feedbacks.Select(t => mapper.Map<FeedbackDto>(t)).ToList();
            return Results.Ok(ApiResponse.Success(result));

        }

        public static async Task<IResult> GetFeedbacksBySlug([FromRoute] string slug, [AsParameters] PagingModel model,[FromServices] IFeedbackRepository feedbackRepository)
        {
            var feedbackQuery = new FeedbackQuery
            {
                UrlSlug = slug
            };
            var feedbackList = await feedbackRepository.GetFeedbacksBySlug(feedbackQuery, model, foods => foods.ProjectToType<FeedbackDto>());
            var paginationResult = new PaginationResult<FeedbackDto>(feedbackList);
            return Results.Ok(ApiResponse.Success(paginationResult));
        }

        private static async Task<IResult> AddFeeback(HttpContext context
            , IFeedbackRepository feedbackRepository
            , IMediaManager mediaManager
            , IMapper mapper
            , IValidator<FeedbackEditModel> validator)

        {
            var model = await FeedbackEditModel.BindAsync(context);

            // check model is valid
            var validationResult = await validator.ValidateAsync(model);
            if (!validationResult.IsValid)
            {
                return Results.Ok(ApiResponse.Fail(HttpStatusCode.BadRequest, validationResult));
            }

            var slug = model.Title.GenerateSlug();

            if (await feedbackRepository.IsFeedbackSlugExistedAsync(0, slug))
            {
                return Results.Ok(ApiResponse.Fail(HttpStatusCode.Conflict, $"Slug '{slug}' đã được sử dụng cho feedback khác"));
            }
            var feedback = model.Id > 0 ? await feedbackRepository.GetFeedbackById(model.Id) : null;
            if (feedback == null)
            {
                feedback = mapper.Map<Feedback>(model);

            }
            feedback.UrlSlug = slug;
            feedback.Title = model.Title;
            feedback.Description = model.Description;
            feedback.Status = model.Status;
            feedback.Email = model.Email;
            feedback.Meta = model.Meta;
            feedback.ShippingDate = DateTime.Now;

            await feedbackRepository.AddOrUpdateFeedback(feedback);

            return Results.Ok(ApiResponse.Success(mapper.Map<FeedbackDto>(feedback), HttpStatusCode.Created));
        }


        private static async Task<IResult> DeleteFeedback(int id, IFeedbackRepository feedbackRepository)
        {
            var status = await feedbackRepository.DeleteFeedback(id);
            return Results.Ok(status ? ApiResponse.Success(HttpStatusCode.NoContent) : ApiResponse.Fail(HttpStatusCode.NotFound, $"không tìm thấy feedback với mã {id}"));
        }
    }
}
