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

    }
}
