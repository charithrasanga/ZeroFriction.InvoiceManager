using ZeroFriction.InvoiceManager.Domain.Invoices;
using ZeroFriction.InvoiceManager.Domain.Invoices.ValueObjects;
using System;
using System.Collections.Generic;
using System.Text;

namespace ZeroFriction.InvoiceManager.Tests.UnitTests.Helpers
{
    public static class InvoiceHelper
    {
        public static Invoice GetInvoice()
        {
            return new Invoice()
            {
                InvoiceId = new InvoiceId(Guid.NewGuid()),
              //  Summary = new TotalAmount("TotalAmount"),
                Description = new Description("Description")
            };
        }

        public static IEnumerable<Invoice> GetInvoices()
        {
            return new List<Invoice>()
            {
                GetInvoice()
            };
        }

    }
}
