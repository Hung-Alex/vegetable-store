using store_vegetable.Core.Entites;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace store_vegetable.Core.DTO
{
    public class CartItemDto
    {
        public int Id { get; set; }// mã sản phẩm

        public string Name { get; set; } // tên sản phẩm

        public string Image { get; set; }// hình ảnh của sản phẩm

        public string UrlSlug { get; set; }//url slug

        public int Price { get; set; }//giá của sản phẩm

        public int Quantity { get; set; } //Số lượng sản phẩm
    }
}
