
namespace ClothShop.Models.CartAssembly
{
    public class Cart
    {
        public string UserId { get; set; }
        public List<CartItem> CartItems { get; set; }
    }
}
