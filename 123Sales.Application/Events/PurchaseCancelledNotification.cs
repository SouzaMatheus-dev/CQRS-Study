using MediatR;

namespace _123Sales.Application.Events
{
    public class PurchaseCancelledNotification : INotification
    {
        public DateTime CancelledDate { get; set; }
        public Guid SaleId { get; set; }
    }
}