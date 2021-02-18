using Microsoft.EntityFrameworkCore;
using MultiTenantClient.Entities.Identity;
using System;
using System.Collections.Generic;
using System.Text;

namespace MultiTenantClient.Entities
{
    public class MultiTenantClientContext : DbContext
    {
        public MultiTenantClientContext(DbContextOptions<MultiTenantClientContext> options) : base(options)
        {

        }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.ApplyConfigurationsFromAssembly(typeof(MultiTenantClientContext).Assembly);
            base.OnModelCreating(modelBuilder);
        }
        public DbSet<UserEntity> UserEntities { get; set; }
        public DbSet<RoleEntity> RoleEntities { get; set; }
        public DbSet<PermissionEntity> PermissionEntities { get; set; }
        public DbSet<User_Role> User_Roles { get; set; }
        public DbSet<User_Permission> User_Permissions { get; set; }
        public DbSet<Role_Permission> Role_Permissions { get; set; }
    }
}
