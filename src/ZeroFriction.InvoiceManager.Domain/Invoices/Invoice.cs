using System.ComponentModel.DataAnnotations;
using ZeroFriction.InvoiceManager.Domain.Invoices.ValueObjects;



namespace ZeroFriction.InvoiceManager.Domain.Invoices
{
    public class Invoice : IAggregateRoot
    {
     
        public InvoiceId InvoiceId { get; set; }

        public Summary Summary { get; set; }

        public Description Description { get; set; }

      
    }
}
