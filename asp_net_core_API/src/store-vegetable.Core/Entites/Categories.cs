using store_vegetable.Core.Contracts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace store_vegetable.Core.Entites
{
    public class Categories : IEntity
    {
        public int Id { get; set; } // mã chủ đề
        public string Name { get; set; } // tên chủ đề
        public string Description { get; set; }// mô tả về chủ đề
        public string UrlSlug { get; set; }// url slug của chủ đề
        public string Image { get; set; } // hình ảnh của chủ đề
        //public bool IsDelete { get; set;}// trường xóa chủ đề
        public bool ShowOnMenu { get; set; }
        public IList<Food> Foods { get; set; }
 
    }
}
