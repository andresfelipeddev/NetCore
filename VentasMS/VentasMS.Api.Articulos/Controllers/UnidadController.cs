using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using VentasMS.Api.Articulos.Application;
using VentasMS.Api.Articulos.Model;

namespace VentasMS.Api.Articulos.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UnidadController : ControllerBase
    {
        private readonly IMediator _mediator;

        public UnidadController(IMediator mediator)
        {
            _mediator = mediator;
        }


        [HttpPost]
        public async Task<ActionResult<string>> Crear(NewUnidad.Execute data)
        {
            return await _mediator.Send(data);
        }

        [HttpGet]
        public async Task<ActionResult<List<UnidadDto>>> ListsUnidades()
        {
            return await _mediator.Send(new ConsultUnidad.Execute());
        }

        [HttpGet("{unidadGuid}")]
        public async Task<ActionResult<UnidadDto>> GetUnidad(string unidadGuid)
        {
            return await _mediator.Send(new OnlyUnidad.Execute { UnidadGuid = unidadGuid });
        }
    }
}
