using FluentValidation;
using MediatR;
using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using VentasMS.Api.Facturas.Model;
using VentasMS.Api.Facturas.Persistency;

namespace VentasMS.Api.Facturas.Application
{
    public class NewInvoice
    {
        /// <summary>
        /// Devuelve una tupla:
        /// true o false : si se pudo crear la factura
        /// int: el numero de la factura
        /// int: el total de la factura
        /// </summary>
        public class Execute : IRequest<(bool, int, int, string)>
        {
            public int NumberInvoice { get; set; }
            public DateTime DateInvoice { get; set; }
            public string Description { get; set; }
            public List<InvoiceDetailDto> Products { get; set; }
        }

        public class InvoiceValidation : AbstractValidator<Execute>
        {
            public InvoiceValidation()
            {
                RuleFor(r => r.NumberInvoice).NotNull().GreaterThan(0);
                RuleFor(r => r.DateInvoice).NotNull().LessThan(DateTime.Now);
                RuleFor(r => r.Products).NotNull();
            }
        }

        public class Manager : IRequestHandler<Execute, (bool, int, int, string)>
        {
            private readonly InvoiceContext _context;
            private readonly ILogger _logger;

            public Manager(InvoiceContext context, ILogger<Manager> logger)
            {
                _context = context;
                _logger = logger;
            }

            public async Task<(bool, int, int, string)> Handle(Execute request, CancellationToken cancellationToken)
            {
                try
                {
                    _logger.LogInformation("Iniciando creacion del objeto Invoice");
                    Invoice invoice = new Invoice
                    {
                        DateInvoice = request.DateInvoice,
                        Description = request.Description,
                        NumberInvoice = request.NumberInvoice
                    };

                    //_context.Invoice.Add(invoice);
                    //await _context.SaveChangesAsync();
                    //_logger.LogInformation("Iniciando creacion del objeto InvoiceDetail");

                    //foreach (var item in request.Products)
                    //{
                    //    InvoiceDetail detail = new InvoiceDetail
                    //    {
                    //        InvoiceId = invoice.InvoiceId,
                    //        ArticuloGuid = item.ArticuloGuid,
                    //        Price = item.Price,
                    //        Quantity = item.Quantity
                    //    };
                    //    _context.InvoiceDetail.Add(detail);

                    //    invoice.TotalInvoice += (detail.Quantity * detail.Price);
                    //}

                    //await _context.SaveChangesAsync();
                    //_logger.LogInformation("Se grabó el Invoice!");


                    #region opcion store procedure con EF CORE

                    //Armamos el detalle como un documento XML

                    StringBuilder sbXML = new StringBuilder();

                    sbXML.AppendLine("<details>");             

                    foreach (var item in request.Products)
                    {
                        sbXML.AppendLine("<detail ");
                        sbXML.AppendLine("\tarticulo= \"" + item.ArticuloGuid + "\"");
                        sbXML.AppendLine("\tprice = \"" + item.Price + "\"");
                        sbXML.AppendLine("\tquantity = \"" + item.Quantity + "\" >");
                        sbXML.AppendLine("</detail>");
                    }

                    sbXML.AppendLine("</details>");

                    //LLAMAMOS EL CONTEXTO PAERA ACCDER ALA BASE DE DATOS
                    var cnn = _context.Database.GetDbConnection();
                    cnn.Open();

                    var cmd = cnn.CreateCommand();
                    //Configurar el comando
                    cmd.CommandText = " exec AdicionarFactura @numberinvoice, @dateinvoice, @description, @products";
                    cmd.CommandType = System.Data.CommandType.Text;
                    cmd.Parameters.Add(new SqlParameter("@numberinvoice", request.NumberInvoice));
                    cmd.Parameters.Add(new SqlParameter("@dateinvoice", request.DateInvoice));
                    cmd.Parameters.Add(new SqlParameter("@description", request.Description));
                    cmd.Parameters.Add(new SqlParameter("@products", sbXML.ToString()));

                    var p = await cmd.ExecuteNonQueryAsync();

                    // Cerramos la coneion ala base de datos
                    cnn.Close();

                    #endregion
                    return (true, invoice.NumberInvoice, invoice.TotalInvoice, string.Empty);
                }
                catch (Exception ex)
                {
                    _logger.LogError(ex.Message,ex);
                    // Librerias para Log
                    // Nlog
                    // Nlog4Net --> .NET Framework
                    // Serilog
                    return (false, 0, 0, ex.Message); 
                }
            }
        }
    }
}
