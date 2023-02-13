using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using ZeroFriction.InvoiceManager.Domain.Invoices;
using ZeroFriction.InvoiceManager.Domain.Invoices.ValueObjects;

/*
 * A view model represents the data that you want to display on 
 * your view/page, whether it be used for static text or for input
 * values (like textboxes and dropdown lists). It is something 
 * different than your domain model. It is a model for the view. 
 */
namespace ZeroFriction.InvoiceManager.Application.ViewModels
{
    public class InvoiceViewModel
    {
        public InvoiceViewModel()
        {

        }
        public InvoiceViewModel(Guid invoiceId, string description, DateTime invoiceDate, List<InvoiceLine> invoiceLines)
        {
            Id = invoiceId.ToString();
            InvoiceLines = invoiceLines.Select(x => new InvoiceLineViewModel
            {
                Qty = x.Qty,
                ItemCode = x.ItemCode.ToString(),
                LineTotal = x.LineTotal.ToDouble(),
                UnitPrice = x.UnitPrice
            }).ToList();
            Description = description;
            TotalAmount = invoiceLines.Sum(x => x.LineTotal.ToDouble());
            InvoiceDate = invoiceDate;
        }
        public string Id { get; set; }
        public DateTime InvoiceDate { get; set; }

        public double TotalAmount { get; set; }

        [Required]
        public string Description { get; set; }

        [Required]
        public List<InvoiceLineViewModel> InvoiceLines { get; set; }
    }


    public class InvoiceLineViewModel
    {
        public InvoiceLineViewModel()
        {

        }

        public InvoiceLineViewModel(int qty, double unitPrice, string itemCode)
        {
            Qty = qty;
            ItemCode = itemCode;
            UnitPrice = unitPrice;
            LineTotal = Qty * UnitPrice;

        }
        public string ItemCode { get; set; }

        public int Qty { get; set; }

        public double UnitPrice { get; set; }

        public double LineTotal { get; set; }
    }


}
