using _123Sales.Application.Commands;
using _123Sales.Application.Events;
using _123Sales.Domain.Interfaces.Repositories;
using MediatR;

namespace _123Sales.Application.Handlers
{
    public class UpdateSaleCommandHandler : IRequestHandler<UpdateSaleCommand, bool>
    {
        private readonly IMediator _mediator;
        private readonly ISaleRepository _saleRepository;

        public UpdateSaleCommandHandler(ISaleRepository saleRepository, IMediator mediator)
        {
            _saleRepository = saleRepository;
            _mediator = mediator;
        }

        public async Task<bool> Handle(UpdateSaleCommand request, CancellationToken cancellationToken)
        {
            var sale = await _saleRepository.GetSaleByIdAsync(request.SaleId);
            if (sale == null) return false;

            sale.SaleDate = request.SaleDate;
            sale.TotalAmount = request.TotalAmount;

            await _saleRepository.UpdateSaleAsync(sale);

            // Publicar evento de "CompraAlterada"
            await _mediator.Publish(new PurchaseUpdatedNotification
            {
                SaleId = sale.Id,
                UpdatedDate = DateTime.Now
            });

            return true;
        }
    }
}