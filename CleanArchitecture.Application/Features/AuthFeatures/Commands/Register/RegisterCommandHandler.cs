using CleanArchitecture.Application.Services;
using CleanArchitecture.Domain.Dtos;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CleanArchitecture.Application.Features.AuthFeatures.Commands.Register
{
    public sealed class RegisterCommandHandler : IRequestHandler<RegisterCommand, MessageResponse>
    {
        private readonly IAuthService _authService;
        public RegisterCommandHandler(IAuthService _authService)
        {
            this._authService = _authService;
        }
        public async Task<MessageResponse> Handle(RegisterCommand request, CancellationToken cancellationToken)
        {
            await _authService.RegisterAsync(request);
            return new MessageResponse("User registered successfully");


        }
    }
}
