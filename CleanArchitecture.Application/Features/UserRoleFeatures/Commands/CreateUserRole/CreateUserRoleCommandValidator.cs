using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CleanArchitecture.Application.Features.UserRoleFeatures.Commands.CreateUserRole
{
    public sealed class CreateUserRoleCommandValidator : AbstractValidator<CreateUserRoleCommand>
    {
        public CreateUserRoleCommandValidator()
        {
            RuleFor(RuleFor => RuleFor.UserId).NotNull().WithMessage("UserId field can't be null");
            RuleFor(RuleFor => RuleFor.UserId).NotEmpty().WithMessage("UserId field can't be empty");
            RuleFor(RuleFor => RuleFor.RoleId).NotNull().WithMessage("RoleId field can't be null");
            RuleFor(RuleFor => RuleFor.RoleId).NotEmpty().WithMessage("RoleId field can't be empty");
        }
    }
}
