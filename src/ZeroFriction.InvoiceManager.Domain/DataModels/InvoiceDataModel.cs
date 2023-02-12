using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ZeroFriction.InvoiceManager.Domain.Invoices.ValueObjects;

namespace ZeroFriction.InvoiceManager.Domain.DataModels
{
    public class InvoiceDataModel
    {
        [Key]
        public int InvoiceId { get; set; }

        public string Summary { get; set; }

        public string Description { get; set; }
    }
}
