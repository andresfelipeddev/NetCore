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
    public class ArticuloController : ControllerBase
    {
        private readonly IMediator _mediator;

        public ArticuloController(IMediator mediator)
        {
            _mediator = mediator;
        }

        [HttpPost]
        public async Task<ActionResult<Unit>> Create(NewArticulo.Execute data)
        {
            return await _mediator.Send(data);
        }
        [HttpGet]
        public async Task<ActionResult<List<ArticuloDto>>> ListArticulos()
        {
            return await _mediator.Send(new ConsultArticulo.ListArticulo());
        }

        [HttpGet("{articuloGuid}")]
        public async Task<ActionResult<ArticuloDto>> GetArticulo(string articuloGuid)
        {
            return await _mediator.Send(new SearchArticulo.OnlyArticulo { ArticuloGuid = articuloGuid });
        }
    }

}
