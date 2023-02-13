using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using ZeroFriction.InvoiceManager.Domain.Invoices.ValueObjects;

namespace ZeroFriction.InvoiceManager.Infrastructure.Converters
{
    public class LineTotalConverter : ValueConverter<LineTotal, double>
    {
        public LineTotalConverter() :
            base(v => v.ToDouble(), v => new LineTotal(v))
        {
        }
    }
}