namespace WebApi.Models
{
    public class CategoryDto
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string UrlSlug { get; set; }
        public string Image { get; set; } // hình ảnh của chủ đề
    }
}
