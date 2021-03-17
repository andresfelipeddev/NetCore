using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using VentasMS.Api.Facturas.Application;
using VentasMS.Api.Facturas.Model;

namespace VentasMS.Api.Facturas.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class InvoiceController : ControllerBase
    {
        private readonly IMediator _mediator;

        public InvoiceController(IMediator mediator)
        {
            _mediator = mediator;
        }

        [HttpPost]
        public async Task<ActionResult<(bool, int, int, string)>> Create(NewInvoice.Execute data)
        {
            return await _mediator.Send(data);
        }

        [HttpGet("{numberInvoice}")]
        public async Task<ActionResult<InvoiceDto>> GetInvoice(int numberInvoice)
        {
            return await _mediator.Send(new ConsultaInvoice.Execute { NumberInvoice = numberInvoice });
        }
    }
}