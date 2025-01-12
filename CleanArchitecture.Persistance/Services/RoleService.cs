using CleanArchitecture.Application.Features.RoleFeatures.Commands.CreateRole;
using CleanArchitecture.Application.Services;
using CleanArchitecture.Domain.Entitites;
using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CleanArchitecture.Persistance.Services
{
    public sealed class RoleService : IRoleService
    {
        private readonly RoleManager<Role> roleManager;
        public RoleService(RoleManager<Role> roleManager)
        {
            this.roleManager = roleManager;
        }
        public async Task CreateRoleAsync(CreateRoleCommand command)
        {
            var existingRole = await roleManager.FindByNameAsync(command.Name);
            if(existingRole != null)
            {
                throw new InvalidOperationException("Role already exists");
            }

            Role role = new()
            {
                Name = command.Name
            };
            await roleManager.CreateAsync(role);
        }
    }
}
