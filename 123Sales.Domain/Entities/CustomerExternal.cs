using System.ComponentModel.DataAnnotations;

namespace _123Sales.Domain.Entities
{
    public class CustomerExternal
    {
        public Guid Id { get; set; }

        [Required]
        [MaxLength(100)]
        public string Name { get; set; }

        public ICollection<Sale> Sales { get; set; } = new List<Sale>();
    }
}