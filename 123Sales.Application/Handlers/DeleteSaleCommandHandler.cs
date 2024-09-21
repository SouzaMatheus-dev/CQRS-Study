using _123Sales.Application.Commands;
using _123Sales.Application.Events;
using _123Sales.Domain.Interfaces.Repositories;
using MediatR;

namespace _123Sales.Application.Handlers
{
    public class DeleteSaleCommandHandler : IRequestHandler<DeleteSaleCommand, bool>
    {
        private readonly IMediator _mediator;
        private readonly ISaleRepository _saleRepository;

        public DeleteSaleCommandHandler(ISaleRepository saleRepository, IMediator mediator)
        {
            _saleRepository = saleRepository;
            _mediator = mediator;
        }

        public async Task<bool> Handle(DeleteSaleCommand request, CancellationToken cancellationToken)
        {
            var sale = await _saleRepository.GetSaleByIdAsync(request.SaleId);
            if (sale == null) return false;

            await _saleRepository.DeleteSaleAsync(request.SaleId);

            // Publicar evento de "CompraCancelada"
            await _mediator.Publish(new PurchaseCancelledNotification
            {
                SaleId = request.SaleId,
                CancelledDate = DateTime.Now
            });

            return true;
        }
    }
}