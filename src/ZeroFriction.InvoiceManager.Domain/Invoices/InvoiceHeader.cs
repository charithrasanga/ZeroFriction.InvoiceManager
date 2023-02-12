using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using ZeroFriction.InvoiceManager.Domain.Invoices.ValueObjects;

namespace ZeroFriction.InvoiceManager.Domain.Invoices
{
    public class InvoiceHeader : IAggregateRoot
    {
     
        public InvoiceId InvoiceId { get; set; }

        public TotalAmount TotalAmount { get; set; }

        public Description Description { get; set; }

        public DateTime InvoiceDate { get; set; }

        List<InvoiceLines> InvoiceLines { get; set; }

    }
}
