using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace VentasMS.Api.Articulos.Model
{
    public class Articulo
    {
        public int ArticuloId { get; set; }
        public string Nombre { get; set; }
        public int PrecioVenta { get; set; }
        public int UnidadId { get; set; }
        public int MarcaId { get; set; }
        public Marca Marca { get; set; }
        public Unidad Unidad { get; set; }
        public string UnidadGuid { get; set; }
        public string MarcaGuid { get; set; }
        public string ArticuloGuid { get; set; }
    }

}

