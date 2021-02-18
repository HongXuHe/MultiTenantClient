using System;
using System.Collections.Generic;
using System.Text;

namespace MultiTenantClient.Entities.Identity
{
    public class User_Permission:BaseEntity
    {
        public Guid UserId { get; set; }
        public Guid PermissionId { get; set; }
        public virtual UserEntity UserEntity { get; set; }
        public virtual PermissionEntity PermissionEntity { get; set; }
    }
}
