using System;

namespace ZeroFriction.InvoiceManager.Domain.Invoices.Commands
{
    public class InvoiceCommand
    {
        public Guid Id { get; set; }

        public string Description { get; set; }

        public string Summary { get; set; }
    }
}
