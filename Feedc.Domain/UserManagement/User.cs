using System.Collections.Generic;
using Feedc.Domain.PersonManagement;
using Microsoft.AspNetCore.Identity;
using System.ComponentModel.DataAnnotations.Schema;

namespace Feedc.Domain.UserManagement
{
    [Table("User")]
    public class User : IdentityUser<int>
    {
        public string Password { get; private set; }
        public string FirstName { get; private set; }

        public virtual ICollection<Person> Persons { get; set; }

        public User()
        {
            Persons = new List<Person>();
        }

        public User(string firstName, string password)
        {
            Password = password;
            FirstName = firstName;
            Persons = new List<Person>();
        }
    }

    public class UserLogin : IdentityUserLogin<int>
    {
        public virtual int Id { get; set; }
    }

    public class UserClaim : IdentityUserClaim<int>
    {
    }

    public class UserRole : IdentityUserRole<int>
    {
        public virtual int Id { get; set; }

        public virtual Role Role { get; set; }

        public virtual User User { get; set; }
    }

}
