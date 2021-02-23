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
        public Guid Id { get; set; } = Guid.NewGuid();
        public string Name { get; set; }
        public DateTime CreateTime { get; set; } = DateTime.UtcNow;
        public string CreateorId { get; set; }
        public DateTime? EditTime { get; set; } = null;
        public string EditorId { get; set; }
        public bool IsEnabled { get; set; } = true;
        public bool SoftDelete { get; set; } = false;
    }
}
