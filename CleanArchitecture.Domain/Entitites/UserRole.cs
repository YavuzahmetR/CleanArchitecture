using CleanArchitecture.Domain.Abstractions;
using Microsoft.EntityFrameworkCore.Metadata;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CleanArchitecture.Domain.Entitites
{
    public sealed class UserRole : Entity
    {
        public string UserId { get; set; } = null!;
        public string RoleId { get; set; } = null!;
        public User User { get; set; } = default!;
        public Role Role { get; set; } = default!;
    }
}
