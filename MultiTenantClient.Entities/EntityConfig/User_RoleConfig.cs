using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using MultiTenantClient.Entities.Identity;
using System;
using System.Collections.Generic;
using System.Text;

namespace MultiTenantClient.Entities.EntityConfig
{
    public class User_RoleConfig : IEntityTypeConfiguration<User_Role>
    {
        public void Configure(EntityTypeBuilder<User_Role> builder)
        {
            builder.HasKey(x => new { x.UserId, x.RoleId });
            builder.HasOne(x => x.UserEntity).WithMany(y=>y.Roles).HasForeignKey(z => z.UserId);
            builder.HasOne(x => x.RoleEntity).WithMany(y => y.Users).HasForeignKey(z => z.RoleId);
        }
    }
}
