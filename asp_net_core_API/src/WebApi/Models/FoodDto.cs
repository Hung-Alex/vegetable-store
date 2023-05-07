using store_vegetable.Core.Entites;

namespace WebApi.Models
{
    public class FoodDto
    {
        public int Id { get; set; }// mã sản phẩm
        public string Name { get; set; } // tên sản phẩm
        public string Unit { get; set; }// đơn vị tính của sản phẩm
        public int Weight { get; set; }// trọng lượng của sản phẩm
        public string Image { get; set; }// hình ảnh của sản phẩm
        public string Description { get; set; }// mô tả về sản phẩm
        public string UrlSlug { get; set; }//url slug
        public int Price { get; set; }//giá của sản phẩm
        public bool ShowOnPage { get; set; }
        public CategoryDto Categories { get; set; }
       
    }
}
