using Helper.Core.User.Exceptions;
using Helper.Core.User.Value_objects;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;

namespace Helper.Core.User
{
    public class User
    {
        public UserId Id { get; private set; }
        public UserEmail Email { get; private set; }
        public UserPassword PasswordHash { get; private set; }
        public UserRole Role { get; private set; } = Value_objects.Role.User;

        public byte[] RowVersion { get; private set; }
        //public IEnumerable<Inquiry.Inquiry> Inquiries => InquiriesRelation;
        //private ICollection<Inquiry.Inquiry> InquiriesRelation = new List<Inquiry.Inquiry>();

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
