using System.Collections.Generic;
using System.Threading.Tasks;
using API.DTO;
using API.Entities;

namespace API.Interfaces
{
    public interface IUserRepository
    {
        void Update(AppUser user);

        Task<bool> SaveAllAsync();

        Task<IEnumerable<AppUser>> GetUsersAsync();

        Task<AppUser> GetUsersByIdAsync(int id);

        Task<AppUser> GetUsersByUserNameAsync(string username);

        Task<IEnumerable<MemberDTo>> GetMembersAsync();

        Task<MemberDTo> GetMemberAsync(string username);



    }
}