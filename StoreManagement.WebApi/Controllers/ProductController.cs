﻿using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace StoreManagement.WebApi.Controllers
{
    [ApiController]
    [Route("v{version:ApiVersion}/[controller]")]
    public class ProductController(IMediator mediator) : ControllerBase
    {
        private readonly IMediator _mediator = mediator;

        [MapToApiVersion("1")]
        [HttpPost("all")]
        public async Task<IActionResult> GetProducts(CancellationToken cancellationToken)
        {
            return Ok();
        }
    }
}
