﻿using CleanArchitecture.Application.Services;
using CleanArchitecture.Domain.Dtos;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CleanArchitecture.Application.Features.RoleFeatures.Commands.CreateRole
{
    public sealed class CreateRoleCommandHandler : IRequestHandler<CreateRoleCommand, MessageResponse>

    {
        private readonly IRoleService roleService;
        public CreateRoleCommandHandler(IRoleService roleService)
        {
            this.roleService = roleService;
        }
        public async Task<MessageResponse> Handle(CreateRoleCommand request, CancellationToken cancellationToken)
        {
            await roleService.CreateRoleAsync(request);
            return new("Role created successfully!");
        }
    }
}
