using CleanArchitecture.Application.Features.UserRoleFeatures.Commands.CreateUserRole;
using CleanArchitecture.Application.Services;
using CleanArchitecture.Domain.Entitites;
using CleanArchitecture.Domain.Repositories;
using GenericRepository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CleanArchitecture.Persistance.Services
{
    public sealed class UserRoleService : IUserRoleService
    {
        private readonly IUserRoleRepository userRepository;
        private readonly IUnitOfWork unitOfWork;
        public UserRoleService(IUserRoleRepository userRepository, IUnitOfWork unitOfWork)
        {
            this.userRepository = userRepository;
            this.unitOfWork = unitOfWork;
        }
        public async Task CreateUserRoleAsync(CreateUserRoleCommand createUserRoleCommand, CancellationToken cancellationToken)
        {

            var existingRole = await userRepository.FirstOrDefaultAsync(x => x.UserId == createUserRoleCommand.UserId && x.RoleId == createUserRoleCommand.RoleId, cancellationToken);

            if (existingRole != null)
            {
                throw new InvalidOperationException("A user cannot have the same role assigned more than once.");
            }

            UserRole userRole = new()
            {
                UserId = createUserRoleCommand.UserId,
                RoleId = createUserRoleCommand.RoleId
            };
            await userRepository.AddAsync(userRole, cancellationToken);
            await unitOfWork.SaveChangesAsync(cancellationToken);
        }
    }
}
