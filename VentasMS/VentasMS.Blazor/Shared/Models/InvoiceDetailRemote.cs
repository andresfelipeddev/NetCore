using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace VentasMS.Blazor.Shared.Models
{
    public class InvoiceDetailRemote
    {
        public string ArticuloGuid { get; set; }

        public int Quantity { get; set; }

        public int Price { get; set; }

        public string NombreArticulo { get; set; }

        public string Unidad { get; set; }

        public string Marca { get; set; }
    }
}
