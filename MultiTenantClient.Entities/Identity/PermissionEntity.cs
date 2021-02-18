using System;
using System.Collections.Generic;
using System.Text;

namespace MultiTenantClient.Entities.Identity
{
    public class PermissionEntity : BaseEntity
    {
        public string PermissionValue { get; set; }
        public string PermissionName { get; set; }

        public List<User_Permission> Users { get; set; } = new List<User_Permission>();
        public List<Role_Permission> Roles { get; set; } = new List<Role_Permission>();
    }
}
