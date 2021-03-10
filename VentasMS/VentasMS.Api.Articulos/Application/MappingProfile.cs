using AutoMapper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using VentasMS.Api.Articulos.Model;

namespace VentasMS.Api.Articulos.Application
{
    public class MappingProfile : Profile
    {
        public MappingProfile()
        {
            CreateMap<Unidad, UnidadDto>();
            CreateMap<Marca, MarcaDto>();
            CreateMap<Articulo, ArticuloDto>();
        }
    }
}
