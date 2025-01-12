using CleanArchitecture.Application.Features.AuthFeatures.Commands.CreateRefreshToken;
using CleanArchitecture.Application.Features.AuthFeatures.Commands.Login;
using CleanArchitecture.Application.Features.AuthFeatures.Commands.Register;
using CleanArchitecture.Domain.Dtos;
using CleanArchitecture.Presentation.Abstraction;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CleanArchitecture.Presentation.Controllers
{
    public sealed class AuthController : ApiController
    {
        public AuthController(IMediator _mediator) : base(_mediator)
        {
        }

        [HttpPost("[action]")]
        [AllowAnonymous]
        public async Task<IActionResult> CreateUserAsync(RegisterCommand registerCommand, CancellationToken cancellationToken)
        {
            MessageResponse response = await _mediator.Send(registerCommand, cancellationToken);
            return Ok(response);
        }

        [HttpPost("[action]")]
        [AllowAnonymous]
        public async Task<IActionResult> LoginAsync(LoginCommand loginCommand, CancellationToken cancellationToken)
        {
           LoginCommandResponse loginCommandResponse = await _mediator.Send(loginCommand, cancellationToken);
            return Ok(loginCommandResponse);
        }

        [HttpPost("[action]")]
        public async Task<IActionResult> CreateTokenAsync(CreateNewTokenByRefreshTokenCommand  createNewTokenByRefreshTokenCommand, CancellationToken cancellationToken)
        {
            LoginCommandResponse createNewTokenByRefreshTokenCommandResponse = 
                await _mediator.Send(createNewTokenByRefreshTokenCommand, cancellationToken);

            return Ok(createNewTokenByRefreshTokenCommandResponse);
        }
    }
}
