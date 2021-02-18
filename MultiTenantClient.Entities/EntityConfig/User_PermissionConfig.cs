using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using MultiTenantClient.Entities.Identity;
using System;
using System.Collections.Generic;
using System.Text;

namespace MultiTenantClient.Entities.EntityConfig
{
    public class User_PermissionConfig : IEntityTypeConfiguration<User_Permission>
    {
        public void Configure(EntityTypeBuilder<User_Permission> builder)
        {
            builder.HasKey(x => new { x.UserId, x.PermissionId });
            builder.HasOne(x => x.UserEntity).WithMany(y => y.Permissions).HasForeignKey(z => z.UserId);
            builder.HasOne(x => x.PermissionEntity).WithMany(y => y.Users).HasForeignKey(z => z.PermissionId);
        }
    }
}
