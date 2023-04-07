using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace store_vegetable.Core.Entites
{
    public class OrderItem
    {
        public int OrderId { get; set; }// mã đơn hàng 
        public Order Order { get; set; }
        public int FoodId { get; set; }// mã sản phẩm 
        public Food Food { get; set; }
        public int Quantity { get; set; }// số lượng của sản phẩm
        public int Price { get; set; }// giá của của món hàng ở thời điểm lúc mua    
    }
}
