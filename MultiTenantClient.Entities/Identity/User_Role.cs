using System;
using System.Collections.Generic;
using System.Text;

namespace MultiTenantClient.Entities.Identity
{
    public class User_Role:BaseEntity
    {
        public Guid UserId { get; set; }
        public Guid RoleId { get; set; }
        public virtual UserEntity UserEntity { get; set; }
        public virtual RoleEntity RoleEntity { get; set; }
    }
}
