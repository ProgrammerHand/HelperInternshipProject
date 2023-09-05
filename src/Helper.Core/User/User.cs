using Helper.Core.User.Exceptions;
using Helper.Core.User.Value_objects;
using Helper.Core.Utility;
using System.Data;

namespace Helper.Core.User
{
    public class User : ISoftDelete, IDataAudit
    {
        public UserId Id { get; private set; }
        public UserEmail Email { get; private set; }
        public UserPassword PasswordHash { get; private set; }
        public UserRole Role { get; private set; } = Value_objects.Role.User;
        //public IEnumerable<Inquiry.Inquiry> Inquiries => InquiriesRelation;
        //private ICollection<Inquiry.Inquiry> InquiriesRelation = new List<Inquiry.Inquiry>();
        public DateTime CreatedAt { get; private set; }
        public DateTime ModifiedAt { get; private set; }
        public bool IsDeleted { get; private set; }
        public DateTime? DeletedAt { get; private set; }

        private User(UserId id, UserEmail email, UserPassword passwordHash)
        {
            Id = id;
            Email = email;
            PasswordHash = passwordHash;
        }
        private User()
        {
            
        }

        public static User CreateUser(UserEmail email, UserPassword passwordHash) 
        {
            UserId id = Guid.NewGuid();
            return new User(id, email, passwordHash);
        }

        public void EvaluateRole() 
        {
            if (Role.Value == Enum.GetValues(typeof(Role)).Cast<Role>().Last())
                throw new HighestRoleException();
            this.Role.Value++;
        }
    }
}
