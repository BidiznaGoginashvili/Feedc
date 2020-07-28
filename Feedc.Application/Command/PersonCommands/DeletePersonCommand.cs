using System.Threading.Tasks;
using Feedc.Domain.PersonManagement;
using Feedc.Application.Infrastructure;
using Feedc.Infrastructure.Database.Repository;
using System.Runtime.InteropServices;

namespace Feedc.Application.Command.PersonCommands
{
    public class DeletePersonCommand : Infrastructure.Command
    {
        public int Id { get; set; }

        public DeletePersonCommand()
        {

        }

        public override async Task<CommandExecutionResult> ExecuteAsync()
        {
            var repository = GetService<IRepository<Person>>();
            var person = repository.GetById(Id);

            if (person == null)
                return await FailAsync();

            if (string.IsNullOrWhiteSpace(person.Phone))
                return await FailAsync();

            repository.Delete(person);

            return await OkAsync();
        }
    }
}
