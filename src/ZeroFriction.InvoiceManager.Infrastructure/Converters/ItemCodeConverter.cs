using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using ZeroFriction.InvoiceManager.Domain.Invoices.ValueObjects;

namespace ZeroFriction.InvoiceManager.Infrastructure.Converters
{
    public class ItemCodeConverter : ValueConverter<ItemCode, string>
    {
        public ItemCodeConverter() :
            base(v => v.ToString(), v => new ItemCode(v))
        {
        }
    }
}