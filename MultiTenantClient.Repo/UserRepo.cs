using AutoMapper;
using MultiTenantClient.Entities.Identity;
using MultiTenantClient.Repo.UOW;
using System;
using System.Collections.Generic;
using System.Text;

namespace MultiTenantClient.Repo
{
   public class UserRepo:RepoBase<UserEntity>,IUserRepo
    {
        public UserRepo(IUnitOfWork unitOfWork, IMapper mapper)
            :base(unitOfWork,mapper)
        {

        }
    }
}
