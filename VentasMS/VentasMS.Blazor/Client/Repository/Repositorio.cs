using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Text.Json;
using System.Threading.Tasks;
using VentasMS.Blazor.Shared.Models;

namespace VentasMS.Blazor.Client.Repository
{
    public class Repositorio : IRepositorio
    {
        private HttpClient _httpClient;
        public Repositorio()   //HttpClient httpClient)
        {
            //_httpClient = httpClient;
        }

        public async Task<InvoiceRemote> GetInvoiceByNumber(int numberInvoice)
        {
            try
            {
                //  _httpClient.BaseAddress = new Uri(@"https://localhost:55122/");

                _httpClient = new HttpClient();
                _httpClient.BaseAddress = new Uri("https://localhost:44371/");
                var response = await _httpClient.GetAsync($"api/Invoice/{numberInvoice}");
                if (response.IsSuccessStatusCode)
                {
                    var content = await response.Content.ReadAsStringAsync();
                    // opcion para evitar porblemas json
                    var options = new JsonSerializerOptions() { PropertyNameCaseInsensitive = true };
                    var result = JsonSerializer.Deserialize<InvoiceRemote>(content, options);
                    return result;
                }
                return new InvoiceRemote();
            }
            catch (Exception ex)
            {
                return new InvoiceRemote();
            }
        }

        public List<InvoiceRemote> GetInvoices()
        {
            return new List<InvoiceRemote>
            {
            new InvoiceRemote() { NumberInvoice= 2021001, DateInvoice= new DateTime(2021, 03, 01),
                                    Description = "Factura Prueba 001",
                Details= new List<InvoiceDetailRemote>{
                    new InvoiceDetailRemote {ArticuloGuid="Articulo0000001", Marca="LA Unica",
                        NombreArticulo = "Producto Blazor 001", Unidad = "UND", Price=12000, Quantity=20 }
                }
            },
            new InvoiceRemote() { NumberInvoice= 2021005, DateInvoice= new DateTime(2021, 03, 05),
                                    Description = "Factura Prueba 020",
                Details= new List<InvoiceDetailRemote>{
                    new InvoiceDetailRemote {ArticuloGuid="XzrrTT20Articulo0000001", Marca="Fino",
                        NombreArticulo = "Azucar Morena", Unidad = "Kilo", Price=15500, Quantity=25 },
                    new InvoiceDetailRemote {ArticuloGuid="ZZZZZZ20Articulo0000001", Marca="Palmera",
                        NombreArticulo = "Sal MArina", Unidad = "Kilo", Price=5500, Quantity=5 }
                }
            }
        };
        }
    }
}