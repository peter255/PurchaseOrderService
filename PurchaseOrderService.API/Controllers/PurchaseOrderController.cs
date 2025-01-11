using MediatR;
using Microsoft.AspNetCore.Mvc;
using PurchaseOrderService.Application.Commands.CreatePurchaseOrder;
using PurchaseOrderService.Application.Commands.DeactivatePurchaseOrder;


namespace PurchaseOrderService.API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class PurchaseOrderController : ControllerBase
    {
        private readonly IMediator _mediator;

        public PurchaseOrderController(IMediator mediator)
        {
            _mediator = mediator;
        }

        [HttpPost]
        public async Task<IActionResult> Create([FromBody] CreatePurchaseOrderCommand command)
        {
            var id = await _mediator.Send(command);
            return Ok(new { Id = id });
        }

        [HttpPut("{id}/deactivate")]
        public async Task<IActionResult> Deactivate(int id)
        {
            await _mediator.Send(new DeactivatePurchaseOrderCommand(id));
            return NoContent();
        }

    }

}

