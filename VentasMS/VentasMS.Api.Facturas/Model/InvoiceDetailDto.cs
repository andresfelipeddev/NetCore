using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace VentasMS.Api.Facturas.Model
{
    public class InvoiceDetailDto
    {
        public string ArticuloGuid { get; set; }
        public int Quantity { get; set; }
        public int Price { get; set; }
    }
}
