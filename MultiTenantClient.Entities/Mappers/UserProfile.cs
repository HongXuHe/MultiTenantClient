using AutoMapper;
using MultiTenantClient.Entities.Dtos;
using MultiTenantClient.Entities.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace MultiTenantClient.Entities.Mappers
{
   public class UserProfile: Profile
    {
        public UserProfile()
        {
            // entity to dto
            CreateMap<UserEntity, UserDto>()
                .ForMember(x => x.Roles, opt => opt.MapFrom(x => x.Roles.Select(y => y.RoleEntity.Name).ToList()))
                .ForMember(x => x.Permissions, opt => opt.MapFrom(x => x.Permissions.Select(y => y.PermissionEntity.PermissionName).ToList()));
            //createdto to entity
            CreateMap<CreateUserDto, UserEntity>();
        }
    }
}
