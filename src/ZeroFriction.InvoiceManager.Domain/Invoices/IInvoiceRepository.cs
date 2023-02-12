using System;
using System.Collections.Generic;
using System.Text;

namespace ZeroFriction.InvoiceManager.Domain.Invoices
{
    public interface IInvoiceRepository : IRepository<InvoiceHeader>
    {
    }
}
