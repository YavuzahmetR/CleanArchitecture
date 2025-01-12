using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CleanArchitecture.Application.Features.AuthFeatures.Commands.CreateRefreshToken
{
    public sealed class CreateNewTokenByRefreshTokenValidator : AbstractValidator<CreateNewTokenByRefreshTokenCommand>
    {
        public CreateNewTokenByRefreshTokenValidator()
        {
            RuleFor(p => p.UserId).NotEmpty().WithMessage("UserId is required");
            RuleFor(p => p.UserId).NotNull().WithMessage("UserId field can't be null");
            RuleFor(p => p.RefreshToken).NotNull().WithMessage("RefreshToken field must be contain at least 3 characters");
            RuleFor(p => p.RefreshToken).NotEmpty().WithMessage("RefreshToken is required");
        }
    }
}
