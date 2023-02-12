using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using ZeroFriction.InvoiceManager.Domain.Invoices.ValueObjects;

namespace ZeroFriction.InvoiceManager.Infrastructure.Converters
{
    public class TotalAmountConverter : ValueConverter<TotalAmount, double>
    {
        public TotalAmountConverter() :
            base(v => v.ToDouble(), v => new TotalAmount(v))
        {
        }
    }
}