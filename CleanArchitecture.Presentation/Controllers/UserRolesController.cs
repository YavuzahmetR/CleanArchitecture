using CleanArchitecture.Application.Features.UserRoleFeatures.Commands.CreateUserRole;
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
    public sealed class UserRolesController : ApiController
    {
        public UserRolesController(IMediator _mediator) : base(_mediator)
        {
        }

        [HttpPost("[action]")]
        public async Task<IActionResult> CreateRoleAsync(CreateUserRoleCommand createUserRoleCommand, CancellationToken cancellationToken)
        {
            MessageResponse response = await _mediator.Send(createUserRoleCommand, cancellationToken);
            return Ok(response);
        }
    }
}
