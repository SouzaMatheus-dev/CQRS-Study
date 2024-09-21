using System.ComponentModel.DataAnnotations;

namespace _123Sales.Domain.Entities
{
    public class ProductExternal
    {
        [Required]
        [MaxLength(100)]
        public string Description { get; set; }

        public Guid Id { get; set; }

        [Required]
        public decimal UnitPrice { get; set; }
    }
}