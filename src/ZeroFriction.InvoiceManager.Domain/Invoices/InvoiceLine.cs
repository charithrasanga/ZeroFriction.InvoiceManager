using ZeroFriction.InvoiceManager.Domain.Invoices.ValueObjects;

namespace ZeroFriction.InvoiceManager.Domain.Invoices
{
    public class InvoiceLine : IAggregateRoot
    {
        public InvoiceLine()
        {
         //   LineTotal = new LineTotal( Qty * UnitPrice);
        }

        public ItemCode ItemCode { get; set; }

        public int Qty { get; set; }

        public double UnitPrice { get; set; }

        public LineTotal LineTotal { get; set; }


    }
}
