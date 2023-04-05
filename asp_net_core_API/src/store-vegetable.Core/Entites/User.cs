using store_vegetable.Core.Contracts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace store_vegetable.Core.Entites
{
    public class User: IEntity
    {

        public int Id { get; set; }// mã người dùng
        public string Name { get; set; } // tên người dùng
        public string Password { get; set; }// mật khẩu
        public string Role { get; set; } // vai trò của người dùng
        
    }
}
