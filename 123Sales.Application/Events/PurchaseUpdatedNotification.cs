using MediatR;

namespace _123Sales.Application.Events
{
    public class PurchaseUpdatedNotification : INotification
    {
        public Guid SaleId { get; set; }
        public DateTime UpdatedDate { get; set; }
    }
}