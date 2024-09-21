using _123Sales.Application.Events;
using MediatR;
using Serilog;

namespace _123Sales.Application.Handlers
{
    public class PurchaseCancelledNotificationHandler : INotificationHandler<PurchaseCancelledNotification>
    {
        public Task Handle(PurchaseCancelledNotification notification, CancellationToken cancellationToken)
        {
            Log.Information("Purchase cancelled: SaleId: {SaleId}, CancelledDate: {CancelledDate}",
                notification.SaleId, notification.CancelledDate);
            return Task.CompletedTask;
        }
    }
}