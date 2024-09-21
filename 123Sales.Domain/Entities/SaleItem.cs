using System.ComponentModel.DataAnnotations;

namespace _123Sales.Domain.Entities
{
    public class SaleItem
    {
        public Guid Id { get; set; }

        public ProductExternal Product { get; set; }

        public Guid ProductId { get; set; }

        [Required]
        public int Quantity { get; set; }

        public Sale Sale { get; set; }

        public Guid SaleId { get; set; }

        public decimal TotalPrice => Quantity * UnitPrice;

        [Required]
        public decimal UnitPrice { get; set; }
    }
}