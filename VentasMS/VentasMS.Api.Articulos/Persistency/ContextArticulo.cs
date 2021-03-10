using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using VentasMS.Api.Articulos.Model;

namespace VentasMS.Api.Articulos.Persistency
{
    public class ContextArticulo : DbContext
    {
        public ContextArticulo(DbContextOptions<ContextArticulo> options)
            : base(options)
        {

        }

        public DbSet<Unidad> Unidad { get; set; }
        public DbSet<Marca> Marca { get; set; }
        public DbSet<Articulo> Articulo { get; set; }
    }
}
