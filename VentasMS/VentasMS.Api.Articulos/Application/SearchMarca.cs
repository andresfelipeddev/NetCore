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
    public class SearchMarca
    {
        public class OnlyMarca : IRequest<MarcaDto>
        {
            public string MarcaGuid { get; set; }
        }

        public class Manager : IRequestHandler<OnlyMarca, MarcaDto>
        {
            private readonly ContextArticulo _context;
            private readonly IMapper _mapper;

            public Manager(ContextArticulo context, IMapper mapper)
            {
                _context = context;
                _mapper = mapper;
            }

            public async Task<MarcaDto> Handle(OnlyMarca request, CancellationToken cancellationToken)
            {
                var marca = await _context.Marca.Where(m => m.MarcaGuid == request.MarcaGuid)
                            .FirstOrDefaultAsync();

                if (marca == null)
                    throw new Exception("No se encontro la marca");

                var marcaDto = _mapper.Map<Marca, MarcaDto>(marca);
                return marcaDto;
            }
        }
    }
}
