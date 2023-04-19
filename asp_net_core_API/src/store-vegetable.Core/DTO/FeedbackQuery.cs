using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace store_vegetable.Core.DTO
{
    public class FeedbackQuery
    {
       
        public string Title { get; set; }// chủ đề
        public string Email { get; set; } // email 
        public string UrlSlug { get; set; }// url slug
        public bool Status { get; set; } // trạng thái  đã xem hay chưa xem
       
    }
}
