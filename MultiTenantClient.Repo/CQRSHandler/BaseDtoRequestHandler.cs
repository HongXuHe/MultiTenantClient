using MediatR;
using MultiTenantClient.Entities;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace MultiTenantClient.Repo.CQRSHandler
{
    public class BaseDtoRequestHandler : IRequestHandler<BaseDto, string>
    {
        public async Task<string> Handle(BaseDto request, CancellationToken cancellationToken)
        {
            return await Task.FromResult<string>(request.Name);
        }
    }
}
