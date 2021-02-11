using System;
using System.Collections.Generic;
using System.Text;

namespace MultiTenantClient.Shared.Modules
{
    public class ObjectAccessor<TType>:IObjectAccessor<TType> 
    {
       public TType Value { get; set; }

        public ObjectAccessor(TType value)
        {
            Value = value;
        }
        public ObjectAccessor()
        {

        }
    }
    public interface IObjectAccessor<TType>
    {
        TType Value { get; set; }
    }
}
