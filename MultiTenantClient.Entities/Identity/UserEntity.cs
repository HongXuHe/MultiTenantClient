using System;
using System.Collections.Generic;
using System.Text;

namespace MultiTenantClient.Entities.Identity
{
    public class UserEntity:BaseEntity
    {
        public string Password { get; set; }
        public string Email { get; set; }

        public List<User_Role> Roles { get; set; } = new List<User_Role>();
        public List<User_Permission> Permissions { get; set; } = new List<User_Permission>();

    }
}
