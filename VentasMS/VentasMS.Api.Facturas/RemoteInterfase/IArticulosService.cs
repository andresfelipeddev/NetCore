using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using VentasMS.Api.Facturas.RemoteModel;

namespace VentasMS.Api.Facturas.RemoteInterfase
{
    public interface IArticulosService
    {
        Task<(bool resultado, ArticuloRemote articulo, string ErrorMessage)> GetArticulo(string articuloGuid);
        Task<(bool resultado, UnidadRemote unidad, string ErrorMessage)> GetUnidad(string unidadGuid);
        Task<(bool resultado, MarcaRemote marca, string ErrorMessage)> GetMarca(string marcaGuid);
    }
}