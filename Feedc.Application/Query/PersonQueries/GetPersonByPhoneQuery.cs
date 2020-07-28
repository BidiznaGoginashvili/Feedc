using System.Linq;
using System.Threading.Tasks;
using Feedc.Domain.PersonManagement;
using Feedc.Application.Infrastructure;
using Feedc.Infrastructure.Database.Repository;

namespace Feedc.Application.Query.PersonQueries
{
    public class GetPersonByPhoneQuery : Query<Person>
    {
        public string Phone { get; set; }
        private IRepository<Person> personRepository = new Repository<Person>();

        public GetPersonByPhoneQuery()
        {

        }

        public GetPersonByPhoneQuery(string phone)
        {
            Phone = phone;
        }

        public override async Task<QueryExecutionResult<Person>> ExecuteAsync()
        {
            var person = personRepository
                  .GetAll()
                  .FirstOrDefault(person => person.Phone == Phone);

            if (person == null)
                return await FailAsync();

            return await OkAsync(person);
        }
    }
}
