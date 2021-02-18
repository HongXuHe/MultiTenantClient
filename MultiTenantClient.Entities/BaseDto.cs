using MediatR;
using MultiTenantClient.Shared.Attributes;
using System;
using System.Collections.Generic;
using System.Text;

namespace MultiTenantClient.Entities
{
   // [MultiTenantAutoMapper(typeof(BaseEntity))]
    public class BaseDto:IRequest<string>
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
    }
}
