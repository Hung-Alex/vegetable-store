using store_vegetable.Core.Entites;

namespace WebApi.Models
{
    public class FoodEditModel
    {
        public int Id { get; set; }
        public string Name { get; set; } // tên sản phẩm
        public string Unit { get; set; }// đơn vị tính của sản phẩm
        public int Weight { get; set; }// trọng lượng của sản phẩm
        public IFormFile ImageFile { get; set; }
        public string Description { get; set; }// mô tả về sản phẩm
        public int Price { get; set; }//giá của sản phẩm
        public bool ShowOnPage { get; set; }
        public int CategoriesId { get; set; }// mã chủ đề 
       
        public static async ValueTask<FoodEditModel> BindAsync(HttpContext context)
        {
            
            var form = await context.Request.ReadFormAsync();
            return new FoodEditModel()
            {
                Id = int.Parse(form["Id"]),
                Name = form["Name"],
                Description = form["Description"],
                ImageFile = form.Files["ImageFile"],
                ShowOnPage = form["ShowOnPage"] != "false",
                CategoriesId = int.Parse(form["CategoriesId"]),
                Weight= int.Parse(form["Weight"]),
                Price= int.Parse(form["Price"]),
                Unit= form["Unit"],

            };
        }
    }
}
