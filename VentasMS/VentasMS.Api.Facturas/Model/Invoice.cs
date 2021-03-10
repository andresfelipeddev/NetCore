using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace VentasMS.Api.Facturas.Model
{
    public class Invoice
    {
        public int InvoiceId { get; set; }
        public int NumberInvoice { get; set; }
        public DateTime DateInvoice { get; set; }
        public int Iva { get; set; }
        public int TotalDiscount { get; set; }
        public int TotalInvoice { get; set; }
        public string Description { get; set; }
    }
}
