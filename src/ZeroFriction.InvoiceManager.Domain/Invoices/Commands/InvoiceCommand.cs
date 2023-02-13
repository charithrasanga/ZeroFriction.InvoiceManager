using System;
using System.Collections.Generic;

namespace ZeroFriction.InvoiceManager.Domain.Invoices.Commands
{
    public class InvoiceCommand
    {
        public Guid Id { get; set; }

        public string Description { get; set; }

        public DateTime InvoiceDate { get; set; }
        public List<InvoiceLine> InvoiceLines { get; set; }

    }
}
