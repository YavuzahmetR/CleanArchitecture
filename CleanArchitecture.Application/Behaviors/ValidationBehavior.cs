using FluentValidation;
using FluentValidation.Results;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CleanArchitecture.Application.NewFolder
{
    public sealed class ValidationBehavior<TRequest, TResponse> :
      IPipelineBehavior<TRequest, TResponse> where TRequest : class, IRequest<TResponse>
    {
        private readonly IEnumerable<IValidator<TRequest>> _validators;

        public ValidationBehavior(IEnumerable<IValidator<TRequest>> _validators)
        {
            this._validators = _validators;
        }

        public async Task<TResponse> Handle(TRequest request, RequestHandlerDelegate<TResponse> next,
            CancellationToken cancellationToken)
        {
            if (!_validators.Any())
            {
                return await next();
            }

            var context = new ValidationContext<TRequest>(request);
            var errorDictionary = _validators.Select(v => v.Validate(context))
                .SelectMany(result => result.Errors)
                .Where(failure => failure != null)
                .GroupBy(failure => failure.PropertyName, failure => failure.ErrorMessage,
                (propertyName, errorMessage) => new { Key = propertyName, Value = errorMessage.Distinct().ToArray() })
                .ToDictionary(s => s.Key, s => s.Value[0]);

            if (errorDictionary.Any())
            {
                var errors = errorDictionary.Select(s => new ValidationFailure
                {
                    PropertyName = s.Key,
                    ErrorMessage = s.Value
                });
                throw new ValidationException(errors);
            }

            return await next();
        }
    }
}
