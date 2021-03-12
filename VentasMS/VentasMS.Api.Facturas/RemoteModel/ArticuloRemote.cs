using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace VentasMS.Api.Facturas.RemoteModel
{
    public class ArticuloRemote
    {
        public string Nombre { get; set; }

        public int PrecioVenta { get; set; }

        public string UnidadGuid { get; set; }

        public string Marcaguid { get; set; }

        public string ArticuloGuid { get; set; }
    }
}
