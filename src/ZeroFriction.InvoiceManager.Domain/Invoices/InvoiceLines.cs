namespace ZeroFriction.InvoiceManager.Domain.Invoices
{
    public class InvoiceLines
    {

        public string ItemCode { get; set; }

        public int Qty { get; set; }

        public double UnitPrice { get; set; }

        public double LineTotal { get; set; }


    }
}
