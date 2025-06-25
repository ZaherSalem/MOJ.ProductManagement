using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace MOJ.ProductManagement.WebApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ApiControllerBase : ControllerBase
    {
        protected readonly IMediator _mediator;

        public ApiControllerBase(IMediator mediator)
        {
            _mediator = mediator;
        }
    }
}
