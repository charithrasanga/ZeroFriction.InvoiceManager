using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using System;
using ZeroFriction.InvoiceManager.Domain.Invoices.ValueObjects;

namespace ZeroFriction.InvoiceManager.Infrastructure.Converters
{
    public class InvoiceIdConverter : ValueConverter<InvoiceId, Guid>
    {
        public InvoiceIdConverter() :
            base(v => v.ToGuid(), v => new InvoiceId(v))
        {
        }
    }
}