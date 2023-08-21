using Helper.Core.User.Exceptions;
using Helper.Core.User.Value_objects;
using Helper.Infrastructure.DAL;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;

namespace Helper.Core.User
{
    public class User : ISoftDelete
    {
        public UserId Id { get; private set; }
        public UserEmail Email { get; private set; }
        public UserPassword PasswordHash { get; private set; }
        public UserRole Role { get; private set; } = Value_objects.Role.User;
        //public IEnumerable<Inquiry.Inquiry> Inquiries => InquiriesRelation;
        //private ICollection<Inquiry.Inquiry> InquiriesRelation = new List<Inquiry.Inquiry>();
        public bool IsDeleted { get; set; }
        public DateTime? DeletedAt { get; set; }

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
