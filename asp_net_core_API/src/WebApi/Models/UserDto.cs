namespace WebApi.Models
{
    public class UserDto
    {
        public int Id { get; set; }// mã người dùng
        public string Name { get; set; } // tên người dùng
        public string Password { get; set; }// mật khẩu
        public string Role { get; set; } // vai trò của người dùng
    }
}
