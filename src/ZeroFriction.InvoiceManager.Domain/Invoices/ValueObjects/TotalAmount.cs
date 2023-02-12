using System;
using System.Collections.Generic;
using System.Text;



namespace ZeroFriction.InvoiceManager.Domain.Invoices.ValueObjects
{
    public readonly struct TotalAmount
    {
        private readonly double _amount;

        public TotalAmount(double amount)
        {
            if (amount<=0)
            {
                throw new ArgumentNullException($"TotalAmount should be grater than zero");
            }

            _amount = amount;
        }

        public override string ToString()
        {
            return _amount.ToString();
        }
        public  double ToDouble()
        {
            return _amount;
        }
    }
}
