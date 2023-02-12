using ZeroFriction.InvoiceManager.Domain.Invoices.ValueObjects;
using System;
using System.Collections.Generic;
using System.Text;
using ZeroFriction.InvoiceManager.Domain.Invoice;


namespace ZeroFriction.InvoiceManager.Infrastructure.Factories
{
    public class InvoiceFactory : Invoice
    {
        public InvoiceFactory()
        {

        }

        public InvoiceFactory(Summary summary, Description description)
        {
            InvoiceId = new InvoiceId(Guid.NewGuid());
            Summary = summary;
            Description = description;
        }
    }
}
