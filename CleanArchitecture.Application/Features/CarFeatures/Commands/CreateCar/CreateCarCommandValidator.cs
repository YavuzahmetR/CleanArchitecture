using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CleanArchitecture.Application.Features.CarFeatures.Commands.CreateCar
{
    public sealed class CreateCarCommandValidator : AbstractValidator<CreateCarCommand>
    {
        public CreateCarCommandValidator()
        {
            RuleFor(p => p.Name).NotEmpty().WithMessage("Name is required");
            RuleFor(p => p.Name).NotNull().WithMessage("Name field can't be null");
            RuleFor(p => p.Name).MinimumLength(3).WithMessage("Name field must be contain at least 3 characters");
            RuleFor(p => p.Model).NotEmpty().WithMessage("Model is required");
            RuleFor(p => p.Model).NotNull().WithMessage("Model field can't be null");
            RuleFor(p => p.Model).MinimumLength(3).WithMessage("Model field must be contain at least 3 characters");
            RuleFor(p => p.EnginePower).NotEmpty().WithMessage("EnginePower is required");
            RuleFor(p => p.EnginePower).NotNull().WithMessage("EnginePower field can't be null");
            RuleFor(p => p.EnginePower).GreaterThan(100).WithMessage("EnginePower must be greater then 100hp");
        }
    }
}
