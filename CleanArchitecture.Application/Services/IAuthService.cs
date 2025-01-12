using CleanArchitecture.Application.Features.AuthFeatures.Commands.CreateRefreshToken;
using CleanArchitecture.Application.Features.AuthFeatures.Commands.Login;
using CleanArchitecture.Application.Features.AuthFeatures.Commands.Register;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CleanArchitecture.Application.Services
{
    public interface IAuthService
    {
        Task RegisterAsync(RegisterCommand registerCommand);
        Task<LoginCommandResponse> LoginAsync(LoginCommand loginCommand, CancellationToken cancellationToken);
        Task<LoginCommandResponse> CreateTokenByRefreshTokenAsync(CreateNewTokenByRefreshTokenCommand loginCommand, CancellationToken cancellationToken);
    }
}
