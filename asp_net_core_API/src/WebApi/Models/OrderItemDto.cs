using store_vegetable.Core.Entites;

namespace WebApi.Models
{
    public class OrderItemDto
    {
       
     
        public FoodDto Food { get; set; }
        public int Quantity { get; set; }// số lượng của sản phẩm
        public int Price { get; set; }// giá của của món hàng ở thời điểm lúc mua   

       
    }
}
