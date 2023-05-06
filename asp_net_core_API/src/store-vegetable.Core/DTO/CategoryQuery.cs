using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace store_vegetable.Core.DTO
{
    public class CategoryQuery
    {
        public string Name { get; set; } // tên chủ đề
        public string UrlSlug { get; set; }// url slug của chủ đề
    }
}
