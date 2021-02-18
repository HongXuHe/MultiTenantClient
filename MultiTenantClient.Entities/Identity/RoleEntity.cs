using System;
using System.Collections.Generic;
using System.Text;

namespace MultiTenantClient.Entities.Identity
{
    public class RoleEntity:BaseEntity
    {
        public List<User_Role> Users { get; set; } = new List<User_Role>();
        public List<Role_Permission> Permissions { get; set; } = new List<Role_Permission>();
    }
}
