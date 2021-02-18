using System;
using MultiTenantClient.AutoMapper;
using MultiTenantClient.Shared.Attributes;

namespace MultiTenantClient.Entities
{
  //  [MultiTenantAutoMapper(typeof(BaseDto))]
    public class BaseEntity
    {
        public Guid Id { get; set; } = Guid.NewGuid();
        public string Name { get; set; }
        public DateTime CreateTime { get; set; } = DateTime.UtcNow;
        public string  CreateorId { get; set; }
        public DateTime? EditTime { get; set; } = null;
        public string EditorId { get; set; }
        public bool IsEnabled { get; set; } = true;
        public bool SoftDelete { get; set; } = false;
    }
}
