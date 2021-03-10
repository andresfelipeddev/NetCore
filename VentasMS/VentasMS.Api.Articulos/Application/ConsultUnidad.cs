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
    public class ConsultUnidad
    {
        public class Execute : IRequest<List<UnidadDto>>
        {
        }

        public class Manager : IRequestHandler<Execute, List<UnidadDto>>
        {
            private readonly ContextArticulo _context;
            private readonly IMapper _mapper;

            public Manager(ContextArticulo context, IMapper mapper)
            {
                _context = context;
                _mapper = mapper;
            }

            public async Task<List<UnidadDto>> Handle(Execute request, CancellationToken cancellationToken)
            {
                var unidades = await _context.Unidad.ToListAsync();
                var unidadesDto = _mapper.Map<List<Unidad>, List<UnidadDto>>(unidades);
                return unidadesDto;
            }
        }
    }
}
