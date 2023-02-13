using ZeroFriction.InvoiceManager.Domain.Invoices;
using ZeroFriction.InvoiceManager.Domain.Invoices.ValueObjects;
using System;
using System.Collections.Generic;
using System.Linq;

using ZeroFriction.InvoiceManager.Infrastructure.Persistence;
using System.Threading.Tasks;
using System.Security.Cryptography.X509Certificates;

namespace ZeroFriction.InvoiceManager.Infrastructure.Repositories
{

    public class InvoiceRepository : IInvoiceRepository
    {
        private readonly ApplicationDbContext _db;

        public InvoiceRepository(ApplicationDbContext db)
        {
            _db = db;
        }

        public Task<Invoice> Add(Invoice invoice)
        {

            var item = _db.Invoices.Add(invoice);
            _db.SaveChanges();

            return Task.FromResult(item.Entity);
        }

        public Task<Invoice> Update(Invoice invoice)
        {

            var updatedInvoice = _db.Invoices.Update(invoice);
            _db.SaveChanges();
            return Task.FromResult(updatedInvoice.Entity);
        }

        public Task<List<Invoice>> FindAll()
        {
            return Task.FromResult(_db.Invoices.ToList());
        }

        public Task<Invoice> FindById(Guid id)
        {
            var invId = new InvoiceId(id);

            var foundInvoice = _db.Invoices.ToList().First(x => x.InvoiceId.ToGuid() == invId.ToGuid());
            return Task.FromResult(foundInvoice);
        }

        public Task Remove(Guid id)
        {
            var foundInvoice = FindById(id).Result;
            _db.Invoices.Remove(foundInvoice);
            _db.SaveChanges();
            return Task.CompletedTask;
        }
    }
}
