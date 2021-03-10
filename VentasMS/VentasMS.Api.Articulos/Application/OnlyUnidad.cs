using AutoMapper;
using MediatR;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using VentasMS.Api.Articulos.Model;
using VentasMS.Api.Articulos.Persistency;

namespace VentasMS.Api.Articulos.Application
{
    public class OnlyUnidad
    {
        public class Execute : IRequest<UnidadDto>
        {
            public string UnidadGuid { get; set; }
        }

        public class Manager : IRequestHandler<Execute, UnidadDto>
        {
            private readonly ContextArticulo _context;
            private readonly IMapper _mapper;

            public Manager(ContextArticulo context, IMapper mapper)
            {
                _context = context;
                _mapper = mapper;
            }

            public async Task<UnidadDto> Handle(Execute request, CancellationToken cancellationToken)
            {
                var unidad = await _context.Unidad.Where(
                       u => u.UnidadGuid == request.UnidadGuid).FirstOrDefaultAsync();

                if (unidad == null)
                    throw new Exception("No se encontro la unidad.");

                var unidadDto = _mapper.Map<Unidad, UnidadDto>(unidad);

                return unidadDto;

            }
        }
    }
}
