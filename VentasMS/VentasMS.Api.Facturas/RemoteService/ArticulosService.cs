using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text.Json;
using System.Threading.Tasks;
using VentasMS.Api.Facturas.RemoteInterfase;
using VentasMS.Api.Facturas.RemoteModel;

namespace VentasMS.Api.Facturas.RemoteService
{
    public class ArticulosService : IArticulosService
    {
        private readonly IHttpClientFactory _httpClient;
        private readonly ILogger<ArticulosService> _logger;

        public ArticulosService(IHttpClientFactory httpClient, ILogger<ArticulosService> logger)
        {
            _httpClient = httpClient;
            _logger = logger;
        }

        public async Task<(bool resultado, ArticuloRemote articulo, string ErrorMessage)> GetArticulo(string articuloGuid)
        {
            try
            {
                var client = _httpClient.CreateClient("Articulos");
                var response = await client.GetAsync($"api/Articulo/{articuloGuid}");
                if (response.IsSuccessStatusCode)
                {
                    var content = await response.Content.ReadAsStringAsync();
                    // opcion para evitar porblemas json
                    var options = new JsonSerializerOptions() { PropertyNameCaseInsensitive = true };
                    var result = JsonSerializer.Deserialize<ArticuloRemote>(content, options);
                    return (true, result, null);
                }
                return (false, null, response.ReasonPhrase);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex.ToString());
                return (false, null, ex.Message);
            }

        }

        public async Task<(bool resultado, MarcaRemote marca, string ErrorMessage)> GetMarca(string marcaGuid)
        {
            try
            {
                var client = _httpClient.CreateClient("Articulos");
                var response = await client.GetAsync($"api/Marca/{marcaGuid}");
                if (response.IsSuccessStatusCode)
                {
                    var content = await response.Content.ReadAsStringAsync();
                    // opcion para evitar porblemas json
                    var options = new JsonSerializerOptions() { PropertyNameCaseInsensitive = true };
                    var result = JsonSerializer.Deserialize<MarcaRemote>(content, options);
                    return (true, result, null);
                }
                return (false, null, response.ReasonPhrase);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex.ToString());
                return (false, null, ex.Message);
            }
        }

        public async Task<(bool resultado, UnidadRemote unidad, string ErrorMessage)> GetUnidad(string unidadGuid)
        {
            try
            {
                var client = _httpClient.CreateClient("Articulos");
                var response = await client.GetAsync($"api/Unidad/{unidadGuid}");
                if (response.IsSuccessStatusCode)
                {
                    var content = await response.Content.ReadAsStringAsync();
                    // opcion para evitar porblemas json
                    var options = new JsonSerializerOptions() { PropertyNameCaseInsensitive = true };
                    var result = JsonSerializer.Deserialize<UnidadRemote>(content, options);
                    return (true, result, null);
                }
                return (false, null, response.ReasonPhrase);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex.ToString());
                return (false, null, ex.Message);
            }
        }
    }
}