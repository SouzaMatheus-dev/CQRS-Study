using System.ComponentModel.DataAnnotations;

namespace _123Sales.Domain.Entities
{
    public class Sale
    {
        public CustomerExternal Customer { get; set; }

        public Guid CustomerId { get; set; }

        public Guid Id { get; set; }

        public bool IsCancelled { get; set; }

        public ICollection<SaleItem> Items { get; set; } = new List<SaleItem>();

        [Required]
        public DateTime SaleDate { get; set; }

        [Required]
        public decimal TotalAmount { get; set; }
    }
}