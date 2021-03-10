using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace VentasMS.Api.Facturas.Model
{
    public class InvoiceDto
    {
        public int NumberInvoice { get; set; }
        public DateTime DateInvoice { get; set; }
        public string  Description { get; set; }
        public List<InvoiceDetailDto> Details { get; set; }
    }
}
