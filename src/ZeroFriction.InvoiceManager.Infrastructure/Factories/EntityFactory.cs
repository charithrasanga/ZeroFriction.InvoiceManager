using ZeroFriction.InvoiceManager.Domain.Invoice;
using ZeroFriction.InvoiceManager.Domain.Invoices.ValueObjects;
using System;
using System.Collections.Generic;
using System.Text;


namespace ZeroFriction.InvoiceManager.Infrastructure.Factories
{
    public class EntityFactory : IInvoiceFactory
    {
        public Invoice CreateInvoiceInstance(Summary summary, Description description)
        {
            return new InvoiceFactory(summary, description);
        }
    }
}
