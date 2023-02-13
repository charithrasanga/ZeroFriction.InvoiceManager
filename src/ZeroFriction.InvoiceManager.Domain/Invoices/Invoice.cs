using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.Design;
using System.Linq;
using ZeroFriction.InvoiceManager.Domain.Invoices.ValueObjects;

namespace ZeroFriction.InvoiceManager.Domain.Invoices
{
    public class Invoice : IAggregateRoot
    {
        public Invoice()
        {
            //TotalAmount = new TotalAmount(InvoiceLines.Sum(x=> x.LineTotal.ToDouble()));
        }
     
        public InvoiceId InvoiceId { get; set; }

        public TotalAmount TotalAmount { get; set; }

        public Description Description { get; set; }

        public DateTime InvoiceDate { get; set; }

        public List<InvoiceLine> InvoiceLines { get; set; }

    }
}
