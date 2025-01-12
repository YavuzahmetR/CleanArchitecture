using CleanArchitecture.Domain.Entitites;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CleanArchitecture.Persistance.Configurations
{
    public sealed class UserRoleConfiguration : IEntityTypeConfiguration<UserRole>
    {
        public void Configure(EntityTypeBuilder<UserRole> builder)
        {
            builder.ToTable("UserRoles");

            // Primary Key
            builder.HasKey(x => x.Id);

            // Foreign Key - User
            builder.HasOne(x => x.User) 
                .WithMany()          
                .HasForeignKey(x => x.UserId) 
                .OnDelete(DeleteBehavior.Cascade);

            // Foreign Key - Role
            builder.HasOne(x => x.Role) 
                .WithMany()            
                .HasForeignKey(x => x.RoleId) 
                .OnDelete(DeleteBehavior.Cascade);
        }
    }
}
