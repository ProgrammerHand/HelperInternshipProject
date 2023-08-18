using Helper.Core.Inquiry.ValueObjects;
using Helper.Core.User.Value_objects;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Helper.Core.User
{
    public interface IUserRepository
    {
        Task AddAsync(User user);
        Task<User> GetByIdAsync(UserId id);
        Task<User> GetByEmailAsync(UserEmail email);
        Task<bool> CheckByEmailAsync(UserEmail email);
    }
}
