using System;
using System.Collections.Generic;
using System.Text;


namespace ZeroFriction.InvoiceManager.Domain.Invoices.ValueObjects
{
    public struct InvoiceId 
    {
        private readonly Guid _invoiceId;

        public InvoiceId(Guid invoiceId)
        {
            if (invoiceId.Equals(Guid.Empty))
                throw new ArgumentNullException($"InvoiceId does not have any value");

            _invoiceId = invoiceId;
        }
       
        public Guid ToGuid()
        {
            return _invoiceId;
        }
    }
}
