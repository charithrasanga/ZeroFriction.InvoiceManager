using System;

namespace ZeroFriction.InvoiceManager.Domain.Invoices.Events
{
    public class InvoiceCreatedEvent : InvoiceEvent
    {
        public InvoiceCreatedEvent(Guid id, string description, string summary)
        {
            Id = id;
            Summary = summary;
            Description = description;
        }
    }
}
