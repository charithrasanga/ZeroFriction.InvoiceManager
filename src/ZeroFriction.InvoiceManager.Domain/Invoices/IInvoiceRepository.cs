using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace ZeroFriction.InvoiceManager.Domain.Invoices
{
    public interface IInvoiceRepository : IRepository<Invoice>
    {
        Task<Invoice> Update(Invoice invoice);
    }
}
