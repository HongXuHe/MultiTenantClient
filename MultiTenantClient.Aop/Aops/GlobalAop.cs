using AspectCore.DynamicProxy;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace MultiTenantClient.Aop.Aops
{
    public class GlobalAop : AbstractInterceptor
    {
        public async override Task Invoke(AspectContext context, AspectDelegate next)
        {
            Console.WriteLine("before excuted");
          await  next(context);
            Console.WriteLine("after excuted");
        }
    }
}
