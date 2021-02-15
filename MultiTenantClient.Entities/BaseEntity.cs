using System;
using MultiTenantClient.AutoMapper;
using MultiTenantClient.Shared.Attributes;

namespace MultiTenantClient.Entities
{
  //  [MultiTenantAutoMapper(typeof(BaseDto))]
    public class BaseEntity
    {
        public string Name { get; set; }
    }
}
