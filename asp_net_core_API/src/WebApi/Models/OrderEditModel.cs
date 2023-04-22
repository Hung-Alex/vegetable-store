using store_vegetable.Core.Entites;

namespace WebApi.Models
{
    public class OrderEditModel
    {
       
        public string Address { get; set; }// địa chỉ nhận hàng
        public string Phone { get; set; }// số điện thoại
       
        public static async ValueTask<OrderEditModel> BindAsync(HttpContext context)
        {

            var form = await context.Request.ReadFormAsync();
            return new OrderEditModel()
            {
                
                Address = form["Address"],
                Phone = form["Phone"],
              

            };
        }
    }
}
