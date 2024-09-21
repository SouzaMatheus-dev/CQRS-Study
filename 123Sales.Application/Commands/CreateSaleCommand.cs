using _123Sales.Application.Dtos;
using MediatR;

namespace _123Sales.Application.Commands
{
    public class CreateSaleCommand : IRequest<Guid>
    {
        public Guid CustomerId { get; set; }
        public List<SaleItemDto> Items { get; set; }
        public DateTime SaleDate { get; set; }
    }
}