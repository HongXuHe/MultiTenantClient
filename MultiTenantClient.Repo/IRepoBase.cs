using System;
using System.Collections.Generic;
using System.Text;

namespace MultiTenantClient.Repo
{
    public interface IRepoBase
    {
        void FindById(string id);
    }
}
