using System.Linq;
using System.Threading.Tasks;
using Feedc.Domain.PersonManagement;
using Feedc.Application.Infrastructure;
using Feedc.Infrastructure.Database.Repository;

namespace Feedc.Application.Command.PersonCommands
{
    public class BoundPhoneCommand : Infrastructure.Command
    {
        public string Phone { get; set; }
        public int PersonId { get; set; }

        public BoundPhoneCommand()
        {

        }

        public override async Task<CommandExecutionResult> ExecuteAsync()
        {
            var repository = GetService<IRepository<Person>>();
            var person = repository.GetById(PersonId);

            if (person == null)
                return await FailAsync();
            if (!Unique(repository))
                return await FailAsync();

            person.Phone = Phone;

            repository.Update(person);

            return await OkAsync();
        }

        public bool Unique(IRepository<Person> repository) => repository.GetAll().Any(person => person.Phone == Phone && person.Id != PersonId) ? false : true;
    }
}
