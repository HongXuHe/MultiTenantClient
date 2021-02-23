using System;
using System.Collections.Generic;
using System.Text;

namespace MultiTenantClient.Entities.Dtos
{
    public class ResponseDto<T>
    {
        public bool Success { get; set; } = true;
        public string Message { get; set; } = string.Empty;
        public T Data { get; set; } = default(T);
    }
}
