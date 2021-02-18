using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using MultiTenantClient.Entities.Identity;
using System;
using System.Collections.Generic;
using System.Text;

namespace MultiTenantClient.Entities.EntityConfig
{
    public class Role_PermissionConfig : IEntityTypeConfiguration<Role_Permission>
    {
        public void Configure(EntityTypeBuilder<Role_Permission> builder)
        {
            builder.HasKey(x => new { x.PermissionId, x.RoleId });
            builder.HasOne(x => x.PermissionEntity).WithMany(y => y.Roles).HasForeignKey(z => z.PermissionId);
            builder.HasOne(x => x.RoleEntity).WithMany(y => y.Permissions).HasForeignKey(z => z.RoleId);
        }
    }
}
