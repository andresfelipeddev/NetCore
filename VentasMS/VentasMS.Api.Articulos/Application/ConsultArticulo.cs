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
    public class ConsultArticulo
    {

        public class ListArticulo : IRequest<List<ArticuloDto>>
        {
        }

        public class Manager : IRequestHandler<ListArticulo, List<ArticuloDto>>
        {
            private readonly ContextArticulo _context;
            private readonly IMapper _mapper;

            public Manager(ContextArticulo context, IMapper mapper)
            {
                _context = context;
                _mapper = mapper;
            }

            public async Task<List<ArticuloDto>> Handle(ListArticulo request, CancellationToken cancellationToken)
            {
                var articulos = await _context.Articulo.ToListAsync();
                var articulosDto = _mapper.Map<List<Articulo>, List<ArticuloDto>>(articulos);
                return articulosDto;
            }
        }
    }

}
