using ZeroFriction.InvoiceManager.Domain.Invoices.Events;
using System.Threading.Tasks;

namespace ZeroFriction.InvoiceManager.Application.Handlers
{
    public class InvoiceEventHandler
    {   // Here you can do whatever you need with this event,
        // you may send http calls to other apis. dispatch notification events via varios techniques
        public async Task HandleInvoiceCreatedEvent(InvoiceCreatedEvent invoiceCreatedEvent)
        {
            // Handle Invoice Created Event
        }

        public async Task HandleInvoiceUpdatedEvent(InvoiceUpdatedEvent invoiceCreatedEvent)
        {
            //Handle Invoice Updated Event
        }

        public async Task HandleInvoiceDeletedEvent(InvoiceDeletedEvent invoiceDeletedEvent)
        {
            // Handle Invoice DeletedEvent
        }
    }
}
