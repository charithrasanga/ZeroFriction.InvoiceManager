using System;
using System.Collections.Generic;
using System.Text;

namespace ZeroFriction.InvoiceManager.Domain.Invoices.Events
{
    public class InvoiceEvent
    {
        public Guid Id { get; set; }

        public string Description { get; set; }

        public string Summary { get; set; }
    }
}
