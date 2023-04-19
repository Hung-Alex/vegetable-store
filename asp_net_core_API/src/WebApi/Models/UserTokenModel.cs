namespace WebApi.Models
{
    public class UserTokenModel
    {
        public int Id { get; set; }
        public string Role { get; set; }
        public string Token { get; set; }
        public DateTime Expired { get; set; }
    }
}
