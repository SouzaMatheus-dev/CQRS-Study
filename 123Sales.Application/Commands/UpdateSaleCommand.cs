using _123Sales.Application.Dtos;
using MediatR;

namespace _123Sales.Application.Commands
{
    public class UpdateSaleCommand : IRequest<bool>
    {
        public List<SaleItemDto> Items { get; set; }
        public DateTime SaleDate { get; set; }
        public Guid SaleId { get; set; }
        public decimal TotalAmount { get; set; }
    }
}