using CleanArchitecture.Application.Features.RoleFeatures.Commands.CreateRole;
using CleanArchitecture.Domain.Dtos;
using CleanArchitecture.Presentation.Abstraction;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CleanArchitecture.Presentation.Controllers
{
    public sealed class RoleController : ApiController
    {
        public RoleController(IMediator _mediator) : base(_mediator)
        {
        }

        [HttpPost("[action]")]
        public async Task<IActionResult> CreateRoleAsync(CreateRoleCommand createRoleCommand, CancellationToken cancellationToken)
        {
            MessageResponse response = await _mediator.Send(createRoleCommand, cancellationToken);
            return Ok(response);
        }

    }
}
