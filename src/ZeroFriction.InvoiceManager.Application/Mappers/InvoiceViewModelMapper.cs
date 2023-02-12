using ZeroFriction.InvoiceManager.Application.ViewModels;
using ZeroFriction.InvoiceManager.Domain.Invoice;
using ZeroFriction.InvoiceManager.Domain.Invoices.Commands;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Http.Extensions;
using System;
using System.Collections.Generic;
using System.Linq;

/*
 * A view model represents the data that you want to display on 
 * your view/page, whether it be used for static text or for input
 * values (like textboxes and dropdown lists). It is something 
 * different than your domain model. So we need to convert the 
 * domain model to a view model to send it to the client (API response)
 * 
 * This is an example of the mapping, you can use whatever you want in
 * your code, Automapper or any similar library to do this conversion.
 */

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
            }
            );

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
