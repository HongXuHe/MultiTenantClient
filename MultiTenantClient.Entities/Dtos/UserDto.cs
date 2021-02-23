using MultiTenantClient.Entities.Identity;
using System;
using System.Collections.Generic;
using System.Text;

namespace MultiTenantClient.Entities.Dtos
{
    public class UserDto:BaseDto
    {
        public string Password { get; set; }
        public string Email { get; set; }
        public List<string> Roles { get; set; } = new List<string>();
        public List<string> Permissions { get; set; } = new List<string>();
    }
}
