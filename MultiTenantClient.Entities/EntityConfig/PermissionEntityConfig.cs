using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using MultiTenantClient.Entities.Identity;
using System;
using System.Collections.Generic;
using System.Text;

namespace MultiTenantClient.Entities.EntityConfig
{
    public class PermissionEntityConfig : IEntityTypeConfiguration<PermissionEntity>
    {
        public void Configure(EntityTypeBuilder<PermissionEntity> builder)
        {
            builder.HasKey(x => x.Id);
            builder.Property(x => x.PermissionName).HasMaxLength(200).IsRequired();
            builder.Property(x => x.PermissionValue).HasMaxLength(200).IsRequired();
        }
    }
}
