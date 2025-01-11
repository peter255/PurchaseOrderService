using MediatR;
using Microsoft.AspNetCore.Mvc;
using PurchaseOrderService.Application.Commands.CreatePurchaseOrder;
using PurchaseOrderService.Application.Commands.DeactivatePurchaseOrder;
using PurchaseOrderService.Application.Queries.GetPurchaseOrder;

namespace PurchaseOrderService.API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class PurchaseOrderController : ControllerBase
    {
        private readonly IMediator _mediator;
        private readonly Serilog.ILogger _logger;

        public PurchaseOrderController(IMediator mediator, Serilog.ILogger logger)
        {
            _logger = logger;
            _mediator = mediator;
        }

        [HttpPost]
        public async Task<IActionResult> Create([FromBody] CreatePurchaseOrderCommand command)
        {
            try
            {
                var id = await _mediator.Send(command);
                return Ok(new { Id = id });
            }
            catch (Exception ex)
            {
                _logger.Error(ex, "Error Create Purchase Order");
                return StatusCode(500, "Error Create Purchase Order");
            }
        }

        [HttpPut("{id}/deactivate")]
        public async Task<IActionResult> Deactivate(int id)
        {
            try
            {
                await _mediator.Send(new DeactivatePurchaseOrderCommand(id));
                return NoContent();
            }
            catch (Exception ex)
            {
                _logger.Error(ex, "Error Deactivate Purchase Order with ID: {Id}", id);
                return StatusCode(500, $"Error Deactivate Purchase Order with ID: {id}");
            }
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetPurchaseOrder(int id)
        {
            _logger.Information("Fetching Purchase Order with ID: {Id}", id);

            try
            {
                var obj = await _mediator.Send(new GetPurchaseOrderQuery(id));
                return Ok(new { PurchaseOrder = obj });
            }
            catch (Exception ex)
            {
                _logger.Error(ex, "Error fetching Purchase Order with ID: {Id}", id);
                return StatusCode(500, $"Error fetching Purchase Order with ID: {id}");
            }
        }
    }
}

