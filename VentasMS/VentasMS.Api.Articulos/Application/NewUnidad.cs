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
    public class NewUnidad
    {
        // Esta clase define los parametros de entrada 
        public class Execute : IRequest<string>
        {
            public string NombreUnidad { get; set; }
        }

        public class NewUnidadValidation : AbstractValidator<Execute>
        {
            public NewUnidadValidation()
            {
                RuleFor(r => r.NombreUnidad ).NotEmpty();
                RuleFor(r => r.NombreUnidad).MinimumLength(3);
                RuleFor(r => r.NombreUnidad).MaximumLength(30);
            }
        }

        public class Manager : IRequestHandler<Execute, string>
        {
            private readonly ContextArticulo _context;

            public Manager(ContextArticulo context)
            {
                _context = context;
            }

            public async Task<string> Handle(Execute request, CancellationToken cancellationToken)
            {

                var unidad = new Unidad {
                    NombreUnidad = request.NombreUnidad,
                    UnidadGuid =  Guid.NewGuid().ToString()
                };

                var newUnit = _context.Unidad.Add(unidad);

                var registros = await _context.SaveChangesAsync();

                if (registros > 0)
                    return newUnit.Entity.UnidadGuid;    // Unit.Value;

                throw new Exception("No se pudo insertar la Unidad");

            }
        }
    }
}
