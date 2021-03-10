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
    public class MarcaController : ControllerBase
    {
        private readonly IMediator _mediator;

        public MarcaController(IMediator mediator)
        {
            _mediator = mediator;
        }

        [HttpPost]
        public async Task<ActionResult<Unit>> Create(NewMarca.Execute data)
        {
            return await _mediator.Send(data);
        }

        [HttpGet]
        public async Task<ActionResult<List<MarcaDto>>> ListMarcas()
        {
            return await _mediator.Send(new ConsultMarca.ListMarca());
        }

        [HttpGet("{marcaGuid}")]
        public async Task<ActionResult<MarcaDto>> GetMarca(string marcaGuid)
        {
            return await _mediator.Send(new SearchMarca.OnlyMarca { MarcaGuid = marcaGuid });
        }
    }
}
