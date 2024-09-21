using MediatR;

namespace _123Sales.Application.Commands
{
    public class CancelItemCommand : IRequest<bool>
    {
        public Guid ItemId { get; set; }
        public Guid SaleId { get; set; }
    }
}