using System;
using System.Collections.Generic;
using System.Text;

namespace ZeroFriction.InvoiceManager.Domain.Invoices.Commands
{
    public class DeleteInvoiceCommand : InvoiceCommand
    {
        public DeleteInvoiceCommand(Guid id)
        {
            Id = id;
        }
    }
}
