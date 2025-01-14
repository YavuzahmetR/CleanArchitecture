using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CleanArchitecture.Domain.Entitites
{
    public sealed class User : IdentityUser<string>
    {
        public User()
        {
            Id = Guid.NewGuid().ToString();
        }
        public string FullName { get; set; } = null!;
        public string? RefreshToken { get; set; }
        public DateTime? RefreshTokenExpiryTime { get; set; }
    }
}
