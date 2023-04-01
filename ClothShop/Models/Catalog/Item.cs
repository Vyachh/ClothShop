using ClothShop.Enum;

namespace ClothShop.Models.Catalog
{
    public class Item
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public Category Category { get; set; }
    }
}
