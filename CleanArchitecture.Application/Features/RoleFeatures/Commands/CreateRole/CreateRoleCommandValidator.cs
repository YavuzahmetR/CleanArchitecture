using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CleanArchitecture.Application.Features.RoleFeatures.Commands.CreateRole
{
    public sealed class CreateRoleCommandValidator : AbstractValidator<CreateRoleCommand>
    {
        public CreateRoleCommandValidator()
        {
            RuleFor(RuleFor => RuleFor.Name).NotNull().WithMessage("Name field can't be null");
            RuleFor(RuleFor => RuleFor.Name).NotEmpty().WithMessage("Name field can't be empty");
        }

    }
}
