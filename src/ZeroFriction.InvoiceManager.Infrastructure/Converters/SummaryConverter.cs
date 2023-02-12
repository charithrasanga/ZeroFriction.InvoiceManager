using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using ZeroFriction.InvoiceManager.Domain.Invoices.ValueObjects;

namespace ZeroFriction.InvoiceManager.Infrastructure.Converters
{
    public class SummaryConverter : ValueConverter<Summary, string>
    {
        public SummaryConverter() :
            base(v => v.ToString(), v => new Summary(v))
        {
        }
    }
}