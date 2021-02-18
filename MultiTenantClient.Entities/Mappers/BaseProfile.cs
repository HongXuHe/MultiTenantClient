using AutoMapper;
using MultiTenantClient.Entities.Dtos;
using MultiTenantClient.Entities.Identity;
using System;
using System.Collections.Generic;
using System.Text;

namespace MultiTenantClient.Entities.Mappers
{
   public class BaseProfile: Profile
    {
        public BaseProfile()
        {
            CreateMap<BaseDto, BaseEntity>().ReverseMap();
            CreateMap<UserEntity, UserDto>().ReverseMap();
        }
    }
}
