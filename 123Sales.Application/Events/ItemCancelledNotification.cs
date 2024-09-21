using MediatR;

namespace _123Sales.Application.Events
{
    public class ItemCancelledNotification : INotification
    {
        public DateTime CancelledDate { get; set; }
        public Guid ItemId { get; set; }
        public Guid SaleId { get; set; }
    }
}