namespace WebApi.Endpoints
{
    public static class FeedbackEnpoints
    {
        public static WebApplication MapFeedbackEnpoints(this WebApplication app)
        {
            var mapbuider = app.MapGroup("/api/Feedbacks");
            return app;
        }
    }
}
