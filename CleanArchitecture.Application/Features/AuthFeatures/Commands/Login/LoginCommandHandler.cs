﻿using CleanArchitecture.Application.Services;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CleanArchitecture.Application.Features.AuthFeatures.Commands.Login
{
    public sealed class LoginCommandHandler : IRequestHandler<LoginCommand, LoginCommandResponse>
    {
        private readonly IAuthService authService;
        public LoginCommandHandler(IAuthService authService)
        {
            this.authService = authService;
        }
        public async Task<LoginCommandResponse> Handle(LoginCommand request, CancellationToken cancellationToken)
        {
           return await authService.LoginAsync(request, cancellationToken);
        }
    }
}
