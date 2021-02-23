using System;
using System.Collections.Generic;
using System.Text;

namespace MultiTenantClient.Entities.Dtos
{
    public class UserLogInDto
    {
        public string Password { get; set; }
        public string Email { get; set; }
    }
}
