using System.ComponentModel.DataAnnotations;

namespace ClothShop.Models.Catalog
{
    public class Size
    {
        [Key]
        public int Id { get; set; }
        public int? Width { get; set; }
        public int? Height { get; set; }
        public int? Depth { get; set; }
    }
}
