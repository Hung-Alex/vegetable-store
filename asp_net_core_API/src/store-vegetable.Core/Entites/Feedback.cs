using store_vegetable.Core.Contracts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using System.Threading.Tasks;

namespace store_vegetable.Core.Entites
{
    public class Feedback : IEntity
    {
        public int Id { get; set; }// mã đánh giá 
        public string Title { get; set; }// chủ đề
        public string Description { get; set; }// mô tả chủ đề
        public string Email { get; set; } // email 
        public string UrlSlug { get; set; }// url slug
        public string Meta { get; set; } //meta
        public DateTime ShippingDate { get; set;}// ngày gửi
        public bool Status { get; set; } // trạng thái  đã xem hay chưa xem
    }
}
