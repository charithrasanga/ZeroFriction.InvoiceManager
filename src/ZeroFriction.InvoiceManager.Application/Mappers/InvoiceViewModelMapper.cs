using ZeroFriction.InvoiceManager.Application.ViewModels;
using ZeroFriction.InvoiceManager.Domain.Invoices;
using ZeroFriction.InvoiceManager.Domain.Invoices.Commands;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Http.Extensions;
using System;
using System.Collections.Generic;
using System.Linq;


namespace ZeroFriction.InvoiceManager.Application.Mappers
{
    public class InvoiceViewModelMapper
    {
        private readonly IHttpContextAccessor _httpContextAccessor;
        
        public InvoiceViewModelMapper(IHttpContextAccessor httpContextAccessor)
        {
            // for demonstration of feature only. may not be used in this assignment
            _httpContextAccessor = httpContextAccessor;
        }

        public IEnumerable<InvoiceViewModel> ConstructFromListOfEntities(IEnumerable<Invoice> invoices)
        {
            var invoicesViewModel = invoices.Select(i => new InvoiceViewModel
            {
                Id = i.InvoiceId.ToGuid().ToString(),
                Description = i.Description.ToString(),
                Summary = i.Summary.ToString()
            });

            return invoicesViewModel;
        }

        public InvoiceViewModel ConstructFromEntity(Invoice invoice)
        {
            return new InvoiceViewModel
            {
                Id = invoice.InvoiceId.ToGuid().ToString(),
                Description = invoice.Description.ToString(),
                Summary = invoice.Summary.ToString(),
            };
        }

        public CreateNewInvoiceCommand ConvertToNewInvoiceCommand(InvoiceViewModel invoiceViewModel)
        {
            return new CreateNewInvoiceCommand(invoiceViewModel.Summary, invoiceViewModel.Description);
        }

        public DeleteInvoiceCommand ConvertToDeleteInvoiceCommand(Guid id)
        {
            return new DeleteInvoiceCommand(id);
        }
    }
}
