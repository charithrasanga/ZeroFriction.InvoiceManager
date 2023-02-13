using ZeroFriction.InvoiceManager.Application.ViewModels;
using ZeroFriction.InvoiceManager.Domain.Invoices;
using ZeroFriction.InvoiceManager.Domain.Invoices.Commands;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Http.Extensions;
using System;
using System.Collections.Generic;
using System.Linq;
using ZeroFriction.InvoiceManager.Domain.Invoices.ValueObjects;
using System.Net.Http.Headers;

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
                InvoiceLines = GetInvoiceLinesViewModel(i.InvoiceLines),
                InvoiceDate= i.InvoiceDate,
                TotalAmount= i.TotalAmount.ToDouble()  
            });

            return invoicesViewModel;
        }

        private List<InvoiceLineViewModel> GetInvoiceLinesViewModel(List<InvoiceLine> invoiceLines)
        {
            var lines = invoiceLines.Select(x => new InvoiceLineViewModel(x.Qty,x.UnitPrice,x.ItemCode.ToString()) { 
            
              
            }).ToList();

            return lines;
        }

        private List<InvoiceLine> GetInvoiceLines(List<InvoiceLineViewModel> invoiceLines)
        {
            var lines = invoiceLines.Select(x => new InvoiceLine
            {
                ItemCode = new ItemCode(x.ItemCode),
                LineTotal =new LineTotal(x.LineTotal),
                Qty=x.Qty,
                UnitPrice=x.UnitPrice,

            }).ToList();

            return lines;
        }

        public InvoiceViewModel ConstructFromEntity(Invoice invoice)
        {
            return new InvoiceViewModel
            {
                Id = invoice.InvoiceId.ToGuid().ToString(),
                Description = invoice.Description.ToString(),
                InvoiceDate = invoice.InvoiceDate,
                TotalAmount = invoice.TotalAmount.ToDouble(),
                InvoiceLines = GetInvoiceLinesViewModel(invoice.InvoiceLines)
            };
        }

        public CreateNewInvoiceCommand ConvertToNewInvoiceCommand(InvoiceViewModel invoiceViewModel)
        {
            return new CreateNewInvoiceCommand(Guid.Parse(invoiceViewModel.Id), invoiceViewModel.InvoiceDate,invoiceViewModel.Description,GetInvoiceLines( invoiceViewModel.InvoiceLines));
        }

        public UpdateInvoiceCommand ConvertToUpdateInvoiceCommand(InvoiceViewModel invoiceViewModel)
        {
            return new UpdateInvoiceCommand(Guid.Parse(invoiceViewModel.Id), invoiceViewModel.InvoiceDate, invoiceViewModel.Description, GetInvoiceLines(invoiceViewModel.InvoiceLines));
        }

        public DeleteInvoiceCommand ConvertToDeleteInvoiceCommand(Guid id)
        {
            return new DeleteInvoiceCommand(id);
        }
    }
}
