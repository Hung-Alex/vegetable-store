using store_vegetable.Core.Entites;

namespace WebApi.Models
{
    public class CategoryEditModel
    {
        public int Id { get; set; }
        public string Name { get; set; } // tên chủ đề
        public string Description { get; set; }// mô tả về chủ đề
        public IFormFile ImageFile { get; set; }
        public bool ShowOnMenu { get; set; }

        public static async ValueTask<CategoryEditModel> BindAsync(HttpContext context)
        {
            var form = await context.Request.ReadFormAsync();
            return new CategoryEditModel()
            {
                Id = int.Parse(form["Id"]),
                Name = form["Name"],
                Description = form["Description"],
                ImageFile = form.Files["ImageFile"],
                ShowOnMenu = form["ShowOnMenu"] != "false",
                          
            };
        }


    }

}
