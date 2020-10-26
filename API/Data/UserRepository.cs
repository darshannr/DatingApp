using Microsoft.EntityFrameworkCore;
using API.Interfaces;
using API.Entities;
using System.Threading.Tasks;
using System.Collections.Generic;
using API.DTO;
using System.Linq;
using AutoMapper.QueryableExtensions;
using AutoMapper;

namespace API.Data
{
    public class UserRepository : IUserRepository

    {
        private readonly DataContext _context;
        private readonly IMapper _mapper;

        public UserRepository(DataContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }
        public void Update(AppUser user)
        {

            _context.Entry(user).State = EntityState.Modified;

        }

        public async Task<bool> SaveAllAsync()
        {
            return await _context.SaveChangesAsync() > 0;
        }

        public async Task<IEnumerable<AppUser>> GetUsersAsync()
        {
            return await _context.Users
                .Include(p => p.Photos).ToListAsync();
        }

        public async Task<AppUser> GetUsersByIdAsync(int id)
        {
            return await _context.Users.FindAsync(id);
        }

        public async Task<AppUser> GetUsersByUserNameAsync(string username)
        {
            return await _context.Users
                .Include(p => p.Photos)
                .SingleOrDefaultAsync(x => x.UserName == username);
        }

        public async Task<IEnumerable<MemberDTo>> GetMembersAsync()
        {
            return await _context.Users.ProjectTo<MemberDTo>(_mapper.ConfigurationProvider).ToListAsync();
        }

        public async Task<MemberDTo> GetMemberAsync(string username)
        {
            return await _context.Users.Where(x => x.UserName == username)
                .ProjectTo<MemberDTo>(_mapper.ConfigurationProvider).SingleOrDefaultAsync();
        }
    }
}