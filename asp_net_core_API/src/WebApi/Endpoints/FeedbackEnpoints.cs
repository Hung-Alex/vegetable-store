using FluentValidation;
using MailKit;
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
            routeGroupBuilder.MapGet("/all", GetFeedbacks)
                            .WithName("GetFeedbacks")
                            .Produces<ApiResponse<FeedbackDto>>()
                            .RequireAuthorization("Admin");

            routeGroupBuilder.MapGet("/", GetFeedbacksByQuery)
                           .WithName("GetFeedbacksByQuery")
                           .Produces<ApiResponse<PaginationResult<FeedbackDto>>>()
                            .RequireAuthorization("Admin");
            routeGroupBuilder.MapGet("/{id:int}", GetFeedbackById)
                           .WithName("GetFeedbackById")
                           .Produces<ApiResponse<PaginationResult<FeedbackDto>>>()
                           .RequireAuthorization("Admin");



            routeGroupBuilder.MapDelete("/{id:int}", DeleteFeedback)
                            .WithName("DeleteFeedback")
                            .Produces(204)
                            .Produces(404)
                            .RequireAuthorization("Admin");

            routeGroupBuilder.MapPost("/", AddFeeback)
                .WithName("AddFeeback")
                .Accepts<FeedbackEditModel>("multipart/form-data")
                .Produces(401)
                .Produces<ApiResponse<FeedbackDto>>()
                .RequireAuthorization("User");

            routeGroupBuilder.MapPut("/{id:int}/feedback", setStatusFeedback)
                             .WithName("setStatusFeedback")
                             .Produces(401)
                             .Produces<ApiResponse<FeedbackDto>>()
                             .RequireAuthorization("Admin");

            routeGroupBuilder.MapPost("/SendMessage", SendMessage)
                             .WithName("SendMessage")
                              .Accepts<MailRequest>("multipart/form-data")
                             .Produces(401)
                             .Produces<ApiResponse>()
                             .RequireAuthorization("Admin");
            return app;
        }
        private static async Task<IResult> SendMessage(HttpContext context,Mail.IMailService mailService)
        {
            var mailRequest = await MailRequest.BlindAsync(context);
            try
            {
                await mailService.SendEmailAsync(mailRequest);
                return Results.Ok(ApiResponse.Success(HttpStatusCode.NoContent));
            }
            catch (Exception ex)
            {
                return Results.Ok(ApiResponse.Fail(HttpStatusCode.BadRequest, $"Gửi thất bại tới {mailRequest.ToEmail}"));
            }
            return Results.Ok(ApiResponse.Success(HttpStatusCode.NoContent));
        }
        private static async Task<IResult> GetFeedbackById([FromRoute] int id, [FromServices] IFeedbackRepository feedbackRepository,IMapper mapper)
        {
            var feedback = await feedbackRepository.GetFeedbackById(id);
            if (feedback == null) return Results.Ok(ApiResponse.Fail(HttpStatusCode.NotFound,$"Không tìm thấy feedback với mã {id}"));
            return Results.Ok(ApiResponse.Success(mapper.Map<FeedbackDto>(feedback)));
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

        public static async Task<IResult> GetFeedbacksByQuery([AsParameters] FeedbackFilterModel filter, [AsParameters] PagingModel model,[FromServices] IFeedbackRepository feedbackRepository,IMapper mapper)
        {
            var feedbackQuery = mapper.Map<FeedbackQuery>(filter);
           
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
