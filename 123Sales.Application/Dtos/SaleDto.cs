namespace _123Sales.Application.Dtos
{
    public class SaleDto
    {
        public Guid CustomerId { get; set; }
        public Guid Id { get; set; }
        public List<SaleItemDto> Items { get; set; }
        public DateTime SaleDate { get; set; }
        public decimal TotalAmount { get; set; }
    }
}