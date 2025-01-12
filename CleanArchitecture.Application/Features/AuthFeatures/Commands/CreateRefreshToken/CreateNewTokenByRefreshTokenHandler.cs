using CleanArchitecture.Application.Features.AuthFeatures.Commands.Login;
using CleanArchitecture.Application.Services;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CleanArchitecture.Application.Features.AuthFeatures.Commands.CreateRefreshToken
{
    public sealed class CreateNewTokenByRefreshTokenHandler : IRequestHandler<CreateNewTokenByRefreshTokenCommand, LoginCommandResponse>
    {
        private readonly IAuthService authService;
        public CreateNewTokenByRefreshTokenHandler(IAuthService authService)
        {
             this.authService = authService;
        }
        public async Task<LoginCommandResponse> Handle(CreateNewTokenByRefreshTokenCommand request, CancellationToken cancellationToken)
        {
            LoginCommandResponse response = await authService.CreateTokenByRefreshTokenAsync(request, cancellationToken);
            return response;
        }
    }
}
