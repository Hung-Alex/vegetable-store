using store_vegetable.Core.Entites;
using System.Runtime.CompilerServices;

namespace WebApi.Models
{
    public class UserEditModel
    {
       
        public string Name { get; set; } // tên người dùng
        public string Password { get; set; }// mật khẩu
        public string Role { get; set; } // vai trò của người dùng

        public static async ValueTask<UserEditModel> BindAsync(HttpContext context)
        {

            var form = await context.Request.ReadFormAsync();
            return new UserEditModel()
            {

                Name = form["Name"],
                Password = form["Password"],
                Role= form["Role"],


            };
        }
    }
}
