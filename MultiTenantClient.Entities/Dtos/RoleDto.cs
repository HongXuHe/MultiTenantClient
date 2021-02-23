using System;
using System.Collections.Generic;
using System.Text;

namespace MultiTenantClient.Entities.Dtos
{
    public class RoleDto:BaseDto
    {
        public List<string> Users { get; set; } = new List<string>();
        public List<string> Permissions { get; set; } = new List<string>();
    }
}
