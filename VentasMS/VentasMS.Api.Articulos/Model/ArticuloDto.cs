using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace VentasMS.Api.Articulos.Model
{
    public class ArticuloDto
    {
        public string Nombre { get; set; }
        public int PrecioVenta { get; set; }
        public string UnidadGuid { get; set; }
        public string MarcaGuid { get; set; }
        public string ArticuloGuid { get; set; }
    }

}
