namespace WebApi.Models
{
    public class FoodFilterModel
    {
        public string Keyword { get; set; }// từ khóa 
        public string UrlSlug { get; set; }//food urlslug

        public string CategorySlug { get; set; }
    }
}
