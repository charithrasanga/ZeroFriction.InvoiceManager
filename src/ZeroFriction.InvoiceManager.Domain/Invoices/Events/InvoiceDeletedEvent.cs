using System;
using System.Collections.Generic;
using System.Text;

namespace ZeroFriction.InvoiceManager.Domain.Invoices.Events
{
    public class InvoiceDeletedEvent : InvoiceEvent
    {
        public InvoiceDeletedEvent(Guid id)
        {
            Id = id;
        }
    }
}
