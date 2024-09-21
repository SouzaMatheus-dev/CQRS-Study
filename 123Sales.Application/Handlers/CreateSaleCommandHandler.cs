using _123Sales.Application.Commands;
using _123Sales.Application.Events;
using _123Sales.Domain.Entities;
using _123Sales.Domain.Interfaces.Repositories;
using MediatR;

namespace _123Sales.Application.Handlers
{
    public class CreateSaleCommandHandler : IRequestHandler<CreateSaleCommand, Guid>
    {
        private readonly IMediator _mediator;
        private readonly ISaleRepository _saleRepository;

        public CreateSaleCommandHandler(ISaleRepository saleRepository, IMediator mediator)
        {
            _saleRepository = saleRepository;
            _mediator = mediator;
        }

        public async Task<Guid> Handle(CreateSaleCommand request, CancellationToken cancellationToken)
        {
            var sale = new Sale
            {
                Id = Guid.NewGuid(),
                CustomerId = request.CustomerId,
                SaleDate = request.SaleDate,
                Items = request.Items.Select(i => new SaleItem
                {
                    ProductId = i.ProductId,
                    Quantity = i.Quantity,
                    UnitPrice = i.UnitPrice
                }).ToList(),
                TotalAmount = request.Items.Sum(i => i.Quantity * i.UnitPrice)
            };

            await _saleRepository.AddSaleAsync(sale);

            // Publicar evento de "CompraCriada"
            await _mediator.Publish(new PurchaseCreatedNotification
            {
                SaleId = sale.Id,
                CustomerId = sale.CustomerId,
                SaleDate = sale.SaleDate
            });

            return sale.Id;
        }
    }
}