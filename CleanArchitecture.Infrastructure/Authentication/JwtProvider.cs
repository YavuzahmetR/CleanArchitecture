using CleanArchitecture.Application.Abstractions;
using CleanArchitecture.Application.Features.AuthFeatures.Commands.Login;
using CleanArchitecture.Domain.Entitites;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;

namespace CleanArchitecture.Infrastructure.Authentication
{
    public sealed class JwtProvider : IJwtProvider
    {
        private readonly JwtOptions jwtOptions;
        private readonly UserManager<User> userManager;
        public JwtProvider(IOptions<JwtOptions> options, UserManager<User> userManager)
        {
            this.jwtOptions = options.Value;
            this.userManager = userManager;
        }
        public async Task<LoginCommandResponse> GenerateJwtToken(User user)
        {
            var claims = new Claim[]
            {
                new Claim(ClaimTypes.NameIdentifier, user.Id),
                new Claim(ClaimTypes.Email, user.Email!),
                new Claim(JwtRegisteredClaimNames.Name, user.UserName!),
                new Claim("FullName" , user.FullName)
            };

            DateTime expireDate = DateTime.Now.AddHours(1);

            JwtSecurityToken securityToken = new(
                 issuer: jwtOptions.Issuer,
                 audience: jwtOptions.Audience,
                 claims: claims,
                 notBefore: DateTime.Now,
                 expires: expireDate,
                 signingCredentials: new SigningCredentials(new SymmetricSecurityKey(Encoding.UTF8.GetBytes(jwtOptions.SecretKey))
                 , SecurityAlgorithms.HmacSha256));

            string token = new JwtSecurityTokenHandler().WriteToken(securityToken);

            string refreshToken = Convert.ToBase64String(RandomNumberGenerator.GetBytes(32));
            user.RefreshToken = refreshToken;
            user.RefreshTokenExpiryTime = expireDate.AddMinutes(15);

            await userManager.UpdateAsync(user);

            LoginCommandResponse loginCommandResponse = new(
                Token: token,
                RefreshToken: refreshToken,
                RefreshTokenExpiryTime: user.RefreshTokenExpiryTime,
                UserId: user.Id);

            return loginCommandResponse;
        }
    }
}
