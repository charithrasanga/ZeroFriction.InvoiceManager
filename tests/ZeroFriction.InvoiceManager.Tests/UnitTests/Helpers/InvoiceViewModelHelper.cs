using System;
using ZeroFriction.InvoiceManager.Application.ViewModels;

namespace ZeroFriction.InvoiceManager.Tests.UnitTests.Helpers
{
    public static class InvoiceViewModelHelper
    {
        public static InvoiceViewModel GetInvoiceViewModel()
        {
            return new InvoiceViewModel
            {
                Id = Guid.NewGuid().ToString(),
                Summary = "TotalAmount",
                Description = "Description"
            };
        }
    }
}
