using System;

namespace ZeroFriction.InvoiceManager.Domain.Invoices.ValueObjects
{
    public readonly struct ItemCode
    {
        private readonly string _itemCode;

        public ItemCode(string text)
        {
            if (string.IsNullOrWhiteSpace(text))
            {
                throw new ArgumentNullException($"Item code can not be blank");
            }

            _itemCode = text;
        }
        public override string ToString()
        {
            return _itemCode;
        }
    }
}
