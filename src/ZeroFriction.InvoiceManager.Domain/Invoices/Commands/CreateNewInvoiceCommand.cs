using System;
using System.Collections.Generic;

namespace ZeroFriction.InvoiceManager.Domain.Invoices.Commands
{
    public class CreateNewInvoiceCommand : InvoiceCommand
    {
        public CreateNewInvoiceCommand(Guid id, DateTime invoiceDate, string description, List<InvoiceLine> invoiceLines)
        {
            InvoiceDate = invoiceDate;
            Description = description;
            InvoiceLines = invoiceLines;
            Id=id;
        }
    }
}
