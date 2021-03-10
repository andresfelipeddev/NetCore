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
    public class ConsultMarca
    {
        public class ListMarca : IRequest<List<MarcaDto>>
        {
        }

        public class Manager : IRequestHandler<ListMarca, List<MarcaDto>>
        {
            private readonly ContextArticulo _context;
            private readonly IMapper _mapper;

            public Manager(ContextArticulo context, IMapper mapper)
            {
                _context = context;
                _mapper = mapper;
            }

            public async Task<List<MarcaDto>> Handle(ListMarca request, CancellationToken cancellationToken)
            {
                var marcas = await _context.Marca.ToListAsync();
                var marcasDto = _mapper.Map<List<Marca>, List<MarcaDto>>(marcas);
                return marcasDto;
            }
        }
    }
}
