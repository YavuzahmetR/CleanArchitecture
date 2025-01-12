using CleanArchitecture.Application.Services;
using CleanArchitecture.Domain.Dtos;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CleanArchitecture.Application.Features.UserRoleFeatures.Commands.CreateUserRole
{
    public sealed class CreateUserRoleCommandHandler : IRequestHandler<CreateUserRoleCommand, MessageResponse>
    {
        private readonly IUserRoleService userRoleService;
        public CreateUserRoleCommandHandler(IUserRoleService userRoleService)
        {
            this.userRoleService = userRoleService;
        }
        public async Task<MessageResponse> Handle(CreateUserRoleCommand request, CancellationToken cancellationToken)
        {
            await userRoleService.CreateUserRoleAsync(request, cancellationToken);
            return new("User Role created successfully!");
        }
    }
}
