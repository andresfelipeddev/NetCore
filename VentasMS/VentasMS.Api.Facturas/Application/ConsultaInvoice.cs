using MediatR;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using VentasMS.Api.Facturas.Model;
using VentasMS.Api.Facturas.Persistency;
using VentasMS.Api.Facturas.RemoteInterfase;

namespace VentasMS.Api.Facturas.Application
{
    public class ConsultaInvoice
    {

        public class Execute : IRequest<InvoiceDto>
        {
            public int NumberInvoice { get; set; }
        }

        public class Manager : IRequestHandler<Execute, InvoiceDto>
        {
            private readonly InvoiceContext _context;
            private readonly IArticulosService service;

            public Manager(InvoiceContext context, IArticulosService articulosService)
            {
                _context = context;
                service = articulosService;
            }

            public async Task<InvoiceDto> Handle(Execute request, CancellationToken cancellationToken)
            {
                var factura = await _context.Invoice.FirstOrDefaultAsync(
                                    i => i.NumberInvoice == request.NumberInvoice);

                if (factura == null)
                    return new InvoiceDto();

                var detalleFactura = await _context.InvoiceDetail.Where(
                        d => d.InvoiceId == factura.InvoiceId).ToListAsync();

                InvoiceDto invoice = new InvoiceDto
                {
                    DateInvoice = factura.DateInvoice,
                    Description = factura.Description,
                    NumberInvoice = factura.NumberInvoice,
                    Details = new List<InvoiceDetailDto>()
                };

                foreach (var detail in detalleFactura)
                {
                    var response = await service.GetArticulo(detail.ArticuloGuid);
                    if (response.resultado)
                    {
                        var articulo = response.articulo;

                        var responseMarca = await service.GetMarca(articulo.Marcaguid);
                        var responseUnidad = await service.GetUnidad(articulo.UnidadGuid);

                        InvoiceDetailDto detalle = new InvoiceDetailDto
                        {
                            ArticuloGuid = articulo.ArticuloGuid,
                            NombreArticulo = articulo.Nombre,
                            Marca = responseMarca.marca.NombreMarca,
                            Unidad = responseUnidad.unidad.NombreUnidad,
                            Price = detail.Price,
                            Quantity = detail.Quantity
                        };
                        invoice.Details.Add(detalle);
                    }
                }
                return invoice;
            }
        }
    }
}