using AutoMapper;
using Microsoft.Extensions.Logging;
using MultiTenantClient.Entities.Identity;
using MultiTenantClient.Repo.UOW;
using System;
using System.Collections.Generic;
using System.Text;

namespace MultiTenantClient.Repo
{
    public class RoleRepo : RepoBase<RoleEntity>, IRoleRepo
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;
        private readonly ILogger<RepoBase<RoleEntity>> _logger;

        public RoleRepo(IUnitOfWork unitOfWork, IMapper mapper, ILogger<RepoBase<RoleEntity>> logger)
            : base(unitOfWork, mapper, logger)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
            _logger = logger;
        }
    }
}
