using System;
using System.Threading.Tasks;
using Feedc.Domain.UserManagement;
using Feedc.Domain.PersonManagement;
using Feedc.Application.Infrastructure;
using Feedc.Infrastructure.Database.Repository;

namespace Feedc.Application.Command.PersonCommands
{
    public class CreatePersonCommand : Infrastructure.Command
    {
        private Feedc.Infrastructure.Database.FeedcContext context;
        private IRepository<User> userRepository = new Repository<User>();
        public int UserId { get; set; }
        public string Phone { get; set; }
        public string LastName { get; set; }
        public string FirstName { get; set; }

        public CreatePersonCommand()
        {
            context = new Feedc.Infrastructure.Database.FeedcContext();
        }

        public CreatePersonCommand(int userId, string phone, string lastName, string firstName)
        {
            FirstName = firstName;
            LastName = lastName;
            Phone = phone;
            UserId = userId;
        }

        public override async Task<CommandExecutionResult> ExecuteAsync()
        {
            try
            {
                var person = new Person(FirstName, LastName, Phone);
                var user = userRepository.GetById(UserId);
                context.Add(person);
                person.User = user;
                context.SaveChanges();

                return await OkAsync();
            }
            catch (Exception exception)
            {
                return await FailAsync();
            }
        }
    }
}
