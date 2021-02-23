using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace MultiTenantClient.Entities.Dtos
{
    public class CreateUserDto
    {
        [Required]
        public string Password { get; set; }

        [Required]
        public string Name { get; set; }
        [Required]
        public string Email { get; set; }
        public List<string> RoleNames { get; set; } = new List<string>();
        public List<string> PermissionNames { get; set; } = new List<string>();
    }
}
