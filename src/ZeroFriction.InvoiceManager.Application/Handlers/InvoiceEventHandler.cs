using ZeroFriction.InvoiceManager.Domain.Invoices.Events;
using System.Threading.Tasks;

namespace ZeroFriction.InvoiceManager.Application.Handlers
{
    public class InvoiceEventHandler
    {
        public async Task HandleInvoiceCreatedEvent(InvoiceCreatedEvent invoiceCreatedEvent)
        {
            // Here you can do whatever you need with this event, you can propagate the data using a queue, calling another API or sending a notification or whatever
            // With this scenario, you are building a event driven architecture with microservices and DDD
        }

        public async Task HandleInvoiceDeletedEvent(InvoiceDeletedEvent invoiceDeletedEvent)
        {
            // Here you can do whatever you need with this event, you can propagate the data using a queue, calling another API or sending a notification or whatever
            // With this scenario, you are building a event driven architecture with microservices and DDD
        }
    }
}
