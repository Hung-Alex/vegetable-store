namespace WebApi.Models
{
    public class UserResponse
    {
        public int Id { get; set; }

        public string Token { get; set; }

        public DateTime Expired { get; set; }

    }
}
