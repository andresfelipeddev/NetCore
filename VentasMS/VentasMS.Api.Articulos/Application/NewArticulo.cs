using FluentValidation;
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
    public class NewArticulo
    {
        public class Execute : IRequest
        {
            public string Nombre        { get; set; }
            public int    PrecioVenta { get; set; }
            public string UnidadGuid    { get; set; }
            public string MarcaGuid     { get; set; }
        }

        public class NewArticuloValidation : AbstractValidator<Execute>
        {
            public NewArticuloValidation()
            {
                RuleFor(r => r.Nombre).NotEmpty();
                RuleFor(r => r.PrecioVenta).NotNull().NotEqual(0).GreaterThan(1);
                RuleFor(r => r.UnidadGuid).NotEmpty().NotEqual(Guid.Empty.ToString());
                RuleFor(r => r.MarcaGuid).NotEmpty().NotEqual(Guid.Empty.ToString());
            }
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
                var articulo = new Articulo
                {
                    ArticuloGuid = Guid.NewGuid().ToString(),
                    MarcaGuid = request.MarcaGuid,
                    Nombre = request.Nombre,
                    PrecioVenta = request.PrecioVenta,
                    UnidadGuid = request.UnidadGuid
                };

                articulo.MarcaId = _context.Marca.Where(m => m.MarcaGuid == request.MarcaGuid)
                                            .FirstOrDefault().MarcaId;

                articulo.UnidadId = _context.Unidad.Where(u => u.UnidadGuid ==
                                            request.UnidadGuid)
                                            .FirstOrDefault().UnidadId;

                _context.Articulo.Add(articulo);
                var reg = await _context.SaveChangesAsync();

                if (reg < 1)
                    throw new Exception("No se pudo agregar el articulo.");

                return Unit.Value;
            }
        }
    }
}


