using System.Linq;
using System.Threading.Tasks;
using Feedc.Domain.PersonManagement;
using Feedc.Application.Infrastructure;
using Feedc.Infrastructure.Database.Repository;

namespace Feedc.Application.Command.PersonCommands
{
    public class AddPersonPhoneCommand : Infrastructure.Command
    {
        public string Phone { get; set; }
        public int PersonId { get; set; }

        private IRepository<Person> personRepository = new Repository<Person>();

        public AddPersonPhoneCommand()
        {

        }

        public AddPersonPhoneCommand(int personId, string phone)
        {
            Phone = phone;
            PersonId = personId;
        }

        public override async Task<CommandExecutionResult> ExecuteAsync()
        {
            var person = personRepository.GetById(PersonId);

            if (person == null)
                return await FailAsync();
            if (!Unique())
                return await FailAsync();

            person.Phone = Phone;

            personRepository.Update(person);

            return await OkAsync();
        }

        public bool Unique() => personRepository.GetAll().Any(person => person.Phone == Phone && person.Id != PersonId) ? false : true;
    }
}
