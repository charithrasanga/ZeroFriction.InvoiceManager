using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using ZeroFriction.InvoiceManager.Domain.Invoices.ValueObjects;

namespace ZeroFriction.InvoiceManager.Infrastructure.Converters
{
    public class DescriptionConverter : ValueConverter<Description, string>
    {
        public DescriptionConverter() :
            base(v => v.ToString(), v => new Description(v))
        {
        }
    }
}