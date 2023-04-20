namespace WebApi.Models
{
    public class CartItemEditModel
    {
        public int Id { get; set; }
        public int Quantity { get; set; }
        public static async ValueTask<CartItemEditModel> BindAsync(HttpContext context)
        {

            var form = await context.Request.ReadFormAsync();
            return new CartItemEditModel()
            {
                Id = int.Parse(form["Id"]),
                Quantity =int.Parse( form["Quantity"])

            };
        }
    }
}
