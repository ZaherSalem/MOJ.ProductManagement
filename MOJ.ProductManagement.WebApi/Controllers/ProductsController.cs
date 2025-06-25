using MediatR;
using Microsoft.AspNetCore.Mvc;
using MOJ.ProductManagement.Application.DTOs.Common;
using MOJ.ProductManagement.Application.DTOs.Product;
using MOJ.ProductManagement.Application.Features.Products.Commands.Add;
using MOJ.ProductManagement.Application.Features.Products.Commands.Delete;
using MOJ.ProductManagement.Application.Features.Products.Commands.Edit;
using MOJ.ProductManagement.Application.Features.Products.Queries.Get;
using MOJ.ProductManagement.Application.Features.Products.Queries.GetProducts;

namespace MOJ.ProductManagement.WebApi.Controllers
{
    public class ProductsController : ApiControllerBase
    {
        public ProductsController(IMediator mediator) : base(mediator) { }

        [HttpPost]
        public async Task<ActionResult<Result<ProductDto>>> CreateProduct([FromBody] CreateProductDto dto)
        {
            var result = await _mediator.Send(new CreateProductCommand(dto));
            if (result.Succeeded)
                return Ok(result);

            return BadRequest(result);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateProduct(int id, [FromBody] UpdateProductDto updateDto)
        {
            var result = await _mediator.Send(new UpdateProductCommand(updateDto));
            if (result.Succeeded)
                return Ok(result);

            return BadRequest(result);
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<ProductDto>> GetProduct(int id)
        {
            var result = await _mediator.Send(new GetProductQuery(id));
            if (result.Succeeded)
                return Ok(result);

            return BadRequest(result);
        }

        [HttpGet]
        public async Task<ActionResult<List<ProductDto>>> GetProducts([FromQuery] PaginatedRequest dto)
        {
            var result = await _mediator.Send(new GetProductsQuery(dto));
            if (result.Succeeded)
                return Ok(result);

            return BadRequest(result);
        }

        [HttpDelete("{id}")]
        public async Task<ActionResult<bool>> DeleteProduct(int id)
        {
            var result = await _mediator.Send(new DeleteProductCommand(id));
            if (result.Succeeded)
                return Ok(result);

            return BadRequest(result);
        }
    }
}
