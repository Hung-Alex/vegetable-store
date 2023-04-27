namespace WebApi.Models
{
    public class MailRequest
    {
        public string ToEmail { get; set; }
        public string Subject { get; set; }
        public string Body { get; set; }
        public IFormFileCollection Attachments { get; set; }
        public static async ValueTask<MailRequest> BlindAsync(HttpContext context)
        {
            var form = await context.Request.ReadFormAsync();
            return new MailRequest()
            {

                ToEmail = form["ToEmail"],
                Subject = form["Subject"],
                Body = form["Body"],
                Attachments = form.Files
                

            };
        }
    }
}
