using store_vegetable.Core.Contracts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace store_vegetable.Core.Entites
{
    public class Order : IEntity
    {
        public int Id { get; set; }// mã đơn hàng
        public string Address { get; set; }// địa chỉ nhận hàng
        public string Phone { get; set; }// số điện thoại
        public bool Status { get; set; }// trạng thái của đơn hàng
        public int Total { get; set; }// tổng tiền của đơn hàng 
        public DateTime OrderDate { get; set; }
        public int UserId { get; set; }// mã người dùng
        public User User { get; set; }
        public IList<OrderItem> Items { get; set;}

    }
}
