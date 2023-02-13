using System;

namespace ZeroFriction.InvoiceManager.Domain.Invoices.Events
{
    public class InvoiceUpdatedEvent : InvoiceEvent
    {
        public InvoiceUpdatedEvent(Guid id, string description)
        {
            Id = id;
            Description = description;
        }
    }
}
