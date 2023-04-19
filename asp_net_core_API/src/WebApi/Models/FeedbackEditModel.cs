namespace WebApi.Models
{
    public class FeedbackEditModel
    {
        public int Id { get; set; }// mã đánh giá 
        public string Title { get; set; }// chủ đề
        public string Description { get; set; }// mô tả chủ đề
        public string Email { get; set; } // email 
    
        public string Meta { get; set; } //meta

        public bool Status { get; set; } // trạng thái  đã xem hay chưa xem
        public static async ValueTask<FeedbackEditModel> BindAsync(HttpContext context)
        {
            var form = await context.Request.ReadFormAsync();
            return new FeedbackEditModel()
            {
                Id = int.Parse(form["Id"]),
                Description = form["Description"],
                Title = form["Title"],
                Email = form["Email"],
                Meta = form["Meta"],
                Status = form["Status"] != "false",

            };
        }
    }
}
