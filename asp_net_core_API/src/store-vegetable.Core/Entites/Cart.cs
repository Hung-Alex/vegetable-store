using store_vegetable.Core.Contracts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace store_vegetable.Core.Entites
{
    public class Cart : IEntity
    {
        public int Id { get; set; } // mã của giỏ hàng 
        
        public int UserId { get; set; }// mã người dùng
        public User User { get; set; }

        public IList<CartItem> CartItems { get; set; }
        
    }
}
