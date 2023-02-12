using ZeroFriction.InvoiceManager.Domain.Invoices.ValueObjects;
using System;
using System.Collections.Generic;
using System.Text;


namespace ZeroFriction.InvoiceManager.Domain.Invoice
{
    public interface IInvoiceFactory
    {
        Invoice CreateInvoiceInstance(Summary summary, Description description);
    }
}
