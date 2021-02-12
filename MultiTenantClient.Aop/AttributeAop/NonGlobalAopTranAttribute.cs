using AspectCore.DynamicProxy;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace MultiTenantClient.Aop.AttributeAop
{
    public class NonGlobalAopTranAttribute : AbstractInterceptorAttribute
    {

        public override Task Invoke(AspectContext context, AspectDelegate next)
        {
            throw new NotImplementedException();
        }
    }
}
