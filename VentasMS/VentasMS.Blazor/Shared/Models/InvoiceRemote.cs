using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace VentasMS.Blazor.Shared.Models
{
    public class InvoiceRemote
    {
        public int NumberInvoice { get; set; }

        public DateTime DateInvoice { get; set; }

        public string Description { get; set; }

        public List<InvoiceDetailRemote> Details { get; set; }
    }
}
