using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CleanArchitecture.Application.Features.AuthFeatures.Commands.Register
{
    public sealed class RegisterCommandValidator : AbstractValidator<RegisterCommand>
    {
        public RegisterCommandValidator()
        {
            RuleFor(p => p.Email).NotEmpty().WithMessage("Name is required");
            RuleFor(p => p.Email).NotNull().WithMessage("Name field can't be null");
            RuleFor(p => p.Email).EmailAddress().WithMessage("Name field must be contain at least 3 characters");
            RuleFor(p => p.Username).NotEmpty().WithMessage("Model is required");
            RuleFor(p => p.Username).NotNull().WithMessage("Model field can't be null");
            RuleFor(p => p.Username).MinimumLength(3).WithMessage("Model field must be contain at least 3 characters");
            RuleFor(p => p.Password).NotEmpty().WithMessage("EnginePower is required");
            RuleFor(p => p.Password).NotNull().WithMessage("EnginePower field can't be null");
            RuleFor(p => p.Password)
            .Matches(@"^(?=.*[a-z])(?=.*[A-Z])(?=.*\d)(?=.*[!@#$%^&*]).{8,15}$")
            .WithMessage("Password must be between 8-15 characters, contain at least one uppercase letter, one lowercase letter, one number, and one special character (!@#$%^&*).");
        }
    }
}
