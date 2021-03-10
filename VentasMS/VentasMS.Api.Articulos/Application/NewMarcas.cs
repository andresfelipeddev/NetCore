using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using VentasMS.Api.Articulos.Model;
using VentasMS.Api.Articulos.Persistency;

namespace VentasMS.Api.Articulos.Application
{
    public class NewMarca
    {

        public class Execute : IRequest
        {
            public string NombreMarca { get; set; }
        }

        public class Manager : IRequestHandler<Execute>
        {
            private readonly ContextArticulo _context;

            public Manager(ContextArticulo context)
            {
                _context = context;
            }

            public async Task<Unit> Handle(Execute request, CancellationToken cancellationToken)
            {
                var marca = new Marca
                {
                    MarcaGuid = Guid.NewGuid().ToString(),
                    NombreMarca = request.NombreMarca
                };

                _context.Marca.Add(marca);
                var reg = await _context.SaveChangesAsync();

                if (reg > 0)
                    return Unit.Value;

                throw new Exception("No se pudo insertar la marca");
            }
        }
    }
}
