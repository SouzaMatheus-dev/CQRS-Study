using _123Sales.Application.Events;
using MediatR;
using Serilog;

namespace _123Sales.Application.Handlers
{
    public class PurchaseUpdatedNotificationHandler : INotificationHandler<PurchaseUpdatedNotification>
    {
        public Task Handle(PurchaseUpdatedNotification notification, CancellationToken cancellationToken)
        {
            Log.Information("Purchase updated: SaleId: {SaleId}, UpdatedDate: {UpdatedDate}",
                notification.SaleId, notification.UpdatedDate);
            return Task.CompletedTask;
        }
    }
}