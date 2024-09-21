using _123Sales.Application.Dtos;
using MediatR;

namespace _123Sales.Application.Queries
{
    public class GetSaleByIdQuery : IRequest<SaleDto>
    {
        public Guid SaleId { get; set; }
    }
}