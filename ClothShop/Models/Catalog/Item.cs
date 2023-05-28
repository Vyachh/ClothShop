using ClothShop.Enum;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ClothShop.Models.Catalog
{
    public class Item
    {
        [Key]
        public int? Id { get; set; } // Id товара
        public string UserId { get; set; } // Id Автора товара
        public string Name { get; set; }
        public string Image { get; set; }
        public int Price { get; set; }
        public string Description { get; set; }
        public int Quantity { get; set; }
        [ForeignKey("Size")]
        public Size SizeFeatures { get; set; }
        public Category Category { get; set; }
    }
}
