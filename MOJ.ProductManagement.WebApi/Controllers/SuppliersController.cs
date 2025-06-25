using MediatR;
using Microsoft.AspNetCore.Mvc;
using MOJ.ProductManagement.Application.DTOs.Common;
using MOJ.ProductManagement.Application.DTOs.Supplier;
using MOJ.ProductManagement.Application.Features.Suppliers.Commands;
using MOJ.SupplierManagement.Application.Features.Suppliers.Queries;

namespace MOJ.ProductManagement.WebApi.Controllers
{
    public class SuppliersController : ApiControllerBase
    {
        public SuppliersController(IMediator mediator) : base(mediator) { }

        [HttpPost]
        public async Task<ActionResult<Result<SupplierDto>>> CreateSupplier([FromBody] CreateSupplierDto dto)
        {
            var result = await _mediator.Send(new CreateSupplierCommand(dto));
            if (result.Succeeded)
                return Ok(result);

            return BadRequest(result);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateSupplier(int id, [FromBody] SupplierDto updateDto)
        {
            var result = await _mediator.Send(new UpdateSupplierCommand(id, updateDto));
            if (result.Succeeded)
                return Ok(result);

            return BadRequest(result);
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<SupplierDto>> GetSupplier(int id)
        {
            var result = await _mediator.Send(new GetSupplierQuery(id));
            if (result.Succeeded)
                return Ok(result);

            return BadRequest(result);
        }

        [HttpGet]
        public async Task<ActionResult<List<SupplierDto>>> GetSuppliers([FromQuery] PaginatedRequest dto)
        {
            var result = await _mediator.Send(new GetSuppliersQuery(dto));
            if (result.Succeeded)
                return Ok(result);

            return BadRequest(result);
        }
    }
}
