using ZeroFriction.InvoiceManager.Domain.Invoice;
using ZeroFriction.InvoiceManager.Domain.Invoices.ValueObjects;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ZeroFriction.InvoiceManager.Domain.Invoice;

namespace ZeroFriction.InvoiceManager.Infrastructure.Repositories
{
 
    public class InvoiceRepository : IInvoiceRepository
    {
        private readonly IInvoiceFactory _invoiceFactory;

        public InvoiceRepository(IInvoiceFactory invoiceFactory)
        {
            _invoiceFactory = invoiceFactory;
        }

        public Task<Invoice> Add(Invoice invoice)
        {
            return Task.FromResult(
                _invoiceFactory.CreateInvoiceInstance(new Summary("summary test"), new Description("description test")));
        }

        public Task<List<Invoice>> FindAll()
        {
            var invoices = Task.FromResult(new List<Invoice> {
                _invoiceFactory.CreateInvoiceInstance(new Summary("summary test"), new Description("description test"))});

            return invoices;
        }

        public Task<Invoice> FindById(Guid id)
        {
            return Task.FromResult(
                _invoiceFactory.CreateInvoiceInstance(new Summary("summary test"), new Description("description test")));
        }

        public Task Remove(Guid id)
        {
            return Task.CompletedTask;
        }
    }
}
