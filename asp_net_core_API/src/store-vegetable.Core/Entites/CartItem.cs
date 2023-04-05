using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace store_vegetable.Core.Entites
{
    public class CartItem
    {

        public int CartId { get; set; }// mã giỏ hàng 
        public Cart Cart { get; set; }
        public int FoodId { get; set; }// mã sản phẩm 
        public Food Food { get; set; }

    }
}
