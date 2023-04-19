namespace WebApi.Models
{
    public class CategoryItem
    {
        public int Id { get; set; } // mã chủ đề
        public string Name { get; set; } // tên chủ đề
        public string Description { get; set; }// mô tả về chủ đề
        public string UrlSlug { get; set; }// url slug của chủ đề
        public string Image { get; set; } // hình ảnh của chủ đề
        public bool ShowOnMenu { get; set; }
    }
}
