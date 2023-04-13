using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace store_vegetable.Core.DTO
{
    public class FoodQuery
    {
        public string Keyword { get; set; }
        public string CategorySlug { get;set; }
        public string UrlSlug { get; set; }//food urlslug
    }
}
