using Feedc.Domain.UserManagement;

namespace Feedc.Domain.PersonManagement
{
    public class Person
    {
        public int Id { get; set; }
        public string Phone { get; set; }
        public string LastName { get; set; }
        public string FirstName { get; set; }

        public virtual User User { get; set; }

        public Person(string phone, string lastName, string firstName)
        {
            Phone = phone;
            LastName = lastName;
            FirstName = firstName;
        }
    }
}
