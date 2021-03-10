using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace VentasMS.Api.Facturas.Model
{
    public class InvoiceDetail
    {
        public int InvoiceDetailId { get; set; }
        public int InvoiceId { get; set; }
        public Invoice Invoice { get; set; }
        public string ArticuloGuid { get; set; }
        public int Quantity { get; set; }
        public int Price { get; set; }
    }
}
