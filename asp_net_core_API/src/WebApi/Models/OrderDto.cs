using store_vegetable.Core.Entites;

namespace WebApi.Models
{
    public class OrderDto
    {
        public int Id { get; set; }// mã đơn hàng
        public string Address { get; set; }// địa chỉ nhận hàng
        public string Phone { get; set; }// số điện thoại
        public bool Status { get; set; }// trạng thái của đơn hàng
        public int Total { get; set; }// tổng tiền của đơn hàng 
        public DateTime OrderDate { get; set; }
       
        public IList<OrderItemDto> Items { get; set; }
    }
}
