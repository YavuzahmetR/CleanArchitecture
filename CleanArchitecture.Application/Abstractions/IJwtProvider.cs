using CleanArchitecture.Application.Features.AuthFeatures.Commands.Login;
using CleanArchitecture.Domain.Entitites;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CleanArchitecture.Application.Abstractions
{
    public interface IJwtProvider
    {
        Task<LoginCommandResponse> GenerateJwtToken(User user);
    }
}
