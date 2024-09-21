using _123Sales.Application.Events;
using MediatR;
using Serilog;

namespace _123Sales.Application.Handlers
{
    public class ItemCancelledNotificationHandler : INotificationHandler<ItemCancelledNotification>
    {
        public Task Handle(ItemCancelledNotification notification, CancellationToken cancellationToken)
        {
            Log.Information("Item cancelled: ItemId: {ItemId}, SaleId: {SaleId}, CancelledDate: {CancelledDate}",
                notification.ItemId, notification.SaleId, notification.CancelledDate);
            return Task.CompletedTask;
        }
    }
}