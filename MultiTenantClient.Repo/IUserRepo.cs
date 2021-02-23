using MultiTenantClient.Entities.Dtos;
using MultiTenantClient.Entities.Identity;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace MultiTenantClient.Repo
{
    public interface IUserRepo : IRepoBase<UserEntity>
    {
        Task<bool> UserExistByEmailAsync(string userEmail);
        Task<bool> LogInAsync(string userEmail, string password);
        Task<UserDto> GetUserByEmailAsync(string userEmail);
        Task<UserDto> GetUserByIdAsync(Guid id);
        
    }
}
