using Microsoft.EntityFrameworkCore;
using Helper.Core.User;
using Helper.Core.User.Value_objects;
using Helper.Core.Inquiry;

namespace Helper.Infrastructure.DAL.Repositories
{
    internal sealed class UserRepository : IUserRepository
    {
        private readonly HelperDbContext _context;
        public UserRepository(HelperDbContext dbContext)
        {
            _context = dbContext;
        }
        public async Task AddAsync(User user)
        {
            await _context.Users.AddAsync(user);
            await Save();
        }

        public async Task<User> GetByIdAsync(UserId id)
        {
            return await _context.Users.FirstOrDefaultAsync(x => x.Id == id);
        }

        public async Task<bool> Save()
        {
            var saved = _context.SaveChangesAsync();
            return await saved > 0 ? true : false;
        }
    }
}
