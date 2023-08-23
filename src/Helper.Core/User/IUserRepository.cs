using Helper.Core.User.Value_objects;

namespace Helper.Core.User
{
    public interface IUserRepository
    {
        Task AddAsync(User user);
        Task<User> GetByIdAsync(UserId id);
        Task<User> GetByEmailAsync(UserEmail email);
        Task<bool> CheckByEmailAsync(UserEmail email);
        Task DeleteUser(User inquiry);
    }
}
