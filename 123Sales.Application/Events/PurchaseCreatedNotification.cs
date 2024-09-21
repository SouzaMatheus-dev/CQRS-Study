using MediatR;

namespace _123Sales.Application.Events
{
    public class PurchaseCreatedNotification : INotification
    {
        public Guid CustomerId { get; set; }
        public DateTime SaleDate { get; set; }
        public Guid SaleId { get; set; }
    }
}