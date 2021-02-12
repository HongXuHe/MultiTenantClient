using AspectCore.DynamicProxy;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace MultiTenantClient.Aop.Aops
{
    /// <summary>
    /// redis aop
    /// </summary>
    public class RedisCacheAop : AbstractInterceptor
    {
        public override async Task Invoke(AspectContext context, AspectDelegate next)
        {
            await next(context);
        }
    }
}
