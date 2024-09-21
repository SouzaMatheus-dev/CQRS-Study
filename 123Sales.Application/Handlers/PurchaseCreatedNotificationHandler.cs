using _123Sales.Application.Events;
using MediatR;
using Serilog;

namespace _123Sales.Application.Handlers
{
    public class PurchaseCreatedNotificationHandler : INotificationHandler<PurchaseCreatedNotification>
    {
        public Task Handle(PurchaseCreatedNotification notification, CancellationToken cancellationToken)
        {
            Log.Information("Purchase created successfully: SaleId: {SaleId}, CustomerId: {CustomerId}, SaleDate: {SaleDate}",
                notification.SaleId, notification.CustomerId, notification.SaleDate);
            return Task.CompletedTask;
        }
    }
}