using System.Threading.Tasks;
using Feedc.Domain.PersonManagement;
using Feedc.Application.Infrastructure;
using Feedc.Infrastructure.Database.Repository;

namespace Feedc.Application.Command.PersonCommands
{
    public class DeletePersonCommand : Infrastructure.Command
    {
        public int Id { get; set; }

        private IRepository<Person> personRepository = new Repository<Person>();

        public DeletePersonCommand()
        {

        }

        public DeletePersonCommand(int id)
        {
            Id = id;
        }

        public override async Task<CommandExecutionResult> ExecuteAsync()
        {
            var person = personRepository.GetById(Id);

            if (person == null)
                return await FailAsync();

            if (string.IsNullOrWhiteSpace(person.Phone))
                return await FailAsync();

            personRepository.Delete(person);

            return await OkAsync();
        }
    }
}
