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
    public class SearchArticulo
    {
        public class OnlyArticulo : IRequest<ArticuloDto>
        {
            public string ArticuloGuid { get; set; }
        }

        public class Manager : IRequestHandler<OnlyArticulo, ArticuloDto>
        {
            private readonly ContextArticulo _context;
            private readonly IMapper _mapper;

            public Manager(ContextArticulo context, IMapper mapper)
            {
                _context = context;
                _mapper = mapper;
            }

            public async Task<ArticuloDto> Handle(OnlyArticulo request, CancellationToken cancellationToken)
            {
                var articulo = await _context.Articulo.Where(a => a.ArticuloGuid ==
                                        request.ArticuloGuid)
                                        .FirstOrDefaultAsync();

                if (articulo == null)
                    throw new Exception("Articulo no existe");

                var articuloDto = _mapper.Map<Articulo, ArticuloDto>(articulo);
                return articuloDto;
            }
        }
    }
    

}
