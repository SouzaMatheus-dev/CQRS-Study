using MediatR;

namespace _123Sales.Application.Commands
{
    public class DeleteSaleCommand : IRequest<bool>
    {
        public Guid SaleId { get; set; }
    }
}