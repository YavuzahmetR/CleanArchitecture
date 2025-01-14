using AutoMapper;
using Azure.Core;
using CleanArchitecture.Application.Abstractions;
using CleanArchitecture.Application.Features.AuthFeatures.Commands.CreateRefreshToken;
using CleanArchitecture.Application.Features.AuthFeatures.Commands.Login;
using CleanArchitecture.Application.Features.AuthFeatures.Commands.Register;
using CleanArchitecture.Application.Services;
using CleanArchitecture.Domain.Entitites;
using CleanArchitecture.Infrastructure.Services;
using CleanArchitecture.Persistance.Context;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;

namespace CleanArchitecture.Persistance.Services
{
    public sealed class AuthService : IAuthService
    {
        private readonly UserManager<User> _userManager;
        private readonly IMapper mapper;
        private readonly IEmailService emailService;
        private readonly IJwtProvider jwtProvider;
        public AuthService(UserManager<User> _userManager, IMapper mapper, IEmailService emailService, IJwtProvider jwtProvider)
        {
            this._userManager = _userManager;
            this.mapper = mapper;
            this.emailService = emailService;
            this.jwtProvider = jwtProvider;
        }

        public async Task<LoginCommandResponse> CreateTokenByRefreshTokenAsync(CreateNewTokenByRefreshTokenCommand loginCommand, CancellationToken cancellationToken)
        {
            User? user = await _userManager.FindByIdAsync(loginCommand.UserId);
            if (user == null)
            {
                throw new Exception("User not found");
            }
            if (user.RefreshToken != loginCommand.RefreshToken)
            {
                throw new Exception("Invalid refresh token");
            }
            if (user.RefreshTokenExpiryTime < DateTime.UtcNow)
            {
                throw new Exception("Refresh token expired");
            }
            LoginCommandResponse loginCommandResponse = await jwtProvider.GenerateJwtToken(user);
            return loginCommandResponse;
        }

        public async Task<LoginCommandResponse> LoginAsync(LoginCommand loginCommand, CancellationToken cancellationToken)
        {
            User? user = await _userManager.Users.Where(
                p => p.UserName == loginCommand.UserNameOrEmail ||
                p.Email == loginCommand.UserNameOrEmail)
                .FirstOrDefaultAsync(cancellationToken);
            if (user == null)
            {
                throw new Exception("User not found");
            }
            bool isPasswordValid = await _userManager.CheckPasswordAsync(user, loginCommand.Password);
            if (!isPasswordValid)
            {
                throw new Exception("Invalid password");
            }
            LoginCommandResponse loginCommandResponse = await jwtProvider.GenerateJwtToken(user);
            return loginCommandResponse;
        }

        public async Task RegisterAsync(RegisterCommand registerCommand)
        {
            User user = mapper.Map<User>(registerCommand);

            IdentityResult result = await _userManager.CreateAsync(user, registerCommand.Password);
            if (!result.Succeeded)
            {
                throw new Exception(result.Errors.First().Description);
            }
            await emailService.SendEmailAsync(registerCommand.Email, "Registration successful", "You have successfully registered to our platform");
        }
    }
}
