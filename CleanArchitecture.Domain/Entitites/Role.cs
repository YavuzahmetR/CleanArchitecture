using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CleanArchitecture.Domain.Entitites
{
    public sealed class Role : IdentityRole<string>
    {
        public Role()
        {
            Id = Guid.NewGuid().ToString();
        }
    }
}
