using System.Collections.Generic;
using System.Threading.Tasks;
using VentasMS.Blazor.Shared.Models;

namespace VentasMS.Blazor.Client.Repository
{
    public interface IRepositorio
    {
        List<InvoiceRemote> GetInvoices();

        Task<InvoiceRemote> GetInvoiceByNumber(int numberInvoice);
    }
}