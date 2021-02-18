using System;
using System.Collections.Generic;
using System.Text;

namespace MultiTenantClient.Entities.Identity
{
    public class Role_Permission:BaseEntity
    {
        public Guid RoleId { get; set; }
        public Guid PermissionId { get; set; }
        public virtual RoleEntity RoleEntity { get; set; }
        public virtual PermissionEntity PermissionEntity { get; set; }
    }
}
