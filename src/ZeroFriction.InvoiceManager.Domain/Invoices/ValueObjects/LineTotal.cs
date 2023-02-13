using System;

namespace ZeroFriction.InvoiceManager.Domain.Invoices.ValueObjects
{
    public readonly struct LineTotal
    {
        private readonly double _amount;

        public LineTotal(double amount)
        {
            if (amount <= 0)
            {
                throw new ArgumentNullException($"Line Total should be grater than zero");
            }

            _amount = amount;
        }

        public override string ToString()
        {
            return _amount.ToString();
        }
        public double ToDouble()
        {
            return _amount;
        }
    }
}
