using CleanArchitecture.Domain.Repositories;
using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;
using Microsoft.EntityFrameworkCore;


namespace CleanArchitecture.Infrastructure.Authorization
{
    public sealed class RoleAttribute : Attribute, IAuthorizationFilter
    {
        private readonly string _role;
        private readonly IUserRoleRepository userRoleRepository;
        public RoleAttribute(IUserRoleRepository userRoleRepository, string _role)
        {
            this.userRoleRepository = userRoleRepository;
            this._role = _role;
        }

        public void OnAuthorization(AuthorizationFilterContext context)
        {
            Claim? userEmailClaim = context.HttpContext.User.FindFirst(ClaimTypes.Email);
            if (userEmailClaim == null)
            {
                context.Result = new UnauthorizedResult();
                return;
            }
            bool userHasRole = userRoleRepository.Where(p => p.User.Email == userEmailClaim.Value).
                Include(p => p.Role).Any(P => P.Role.Name == _role);
            if (!userHasRole)
            {
                context.Result = new UnauthorizedResult();
                return;
            }
        }
    }
}
