using _123Sales.Application.Commands;
using _123Sales.Application.Events;
using _123Sales.Domain.Interfaces.Repositories;
using MediatR;

namespace _123Sales.Application.Handlers
{
    public class CancelItemCommandHandler : IRequestHandler<CancelItemCommand, bool>
    {
        private readonly IMediator _mediator;
        private readonly ISaleRepository _saleRepository;

        public CancelItemCommandHandler(ISaleRepository saleRepository, IMediator mediator)
        {
            _saleRepository = saleRepository;
            _mediator = mediator;
        }

        public async Task<bool> Handle(CancelItemCommand request, CancellationToken cancellationToken)
        {
            var sale = await _saleRepository.GetSaleByIdAsync(request.SaleId);
            if (sale == null) return false;

            var item = sale.Items.FirstOrDefault(i => i.Id == request.ItemId);
            if (item == null) return false;

            sale.Items.Remove(item);
            await _saleRepository.UpdateSaleAsync(sale);

            // Publicar evento de "ItemCancelado"
            await _mediator.Publish(new ItemCancelledNotification
            {
                ItemId = request.ItemId,
                SaleId = request.SaleId,
                CancelledDate = DateTime.Now
            });

            return true;
        }
    }
}