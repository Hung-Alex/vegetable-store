using store_vegetable.Core.Contracts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace store_vegetable.Core.Entites
{
    public class Food: IEntity
    {
        public int Id { get; set; }// mã sản phẩm
        public string Name { get; set; } // tên sản phẩm
        public string Unit { get; set; }// đơn vị tính của sản phẩm
        public int Weight { get; set; }// trọng lượng của sản phẩm
        public string Image { get; set; }// hình ảnh của sản phẩm
        public string Description { get; set; }// mô tả về sản phẩm
        public string UrlSlug { get; set; }//url slug
        public int Price { get; set; }//giá của sản phẩm
        //public bool IsDelete { get; set; }//trường xóa sản phẩm
        public bool ShowOnPage { get; set; }
        public int CategoriesId { get; set; }// mã chủ đề 
        public Categories Categories { get; set; }
        public IList<CartItem> CartItems { get; set; }
        public IList<OrderItem> Items { get; set; }


    }
}
