using System;
using System.Collections.Generic;
using System.Text;

namespace MultiTenantClient.Entities.Dtos
{
    public class PermissionDto:BaseDto
    {
        public string PermissionValue { get; set; }
        public string PermissionName { get; set; }
        public List<string> Users { get; set; } = new List<string>();
        public List<string> Roles { get; set; } = new List<string>();
    }
}
