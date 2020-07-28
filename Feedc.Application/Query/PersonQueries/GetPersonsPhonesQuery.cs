using System.Linq;
using System.Threading.Tasks;
using System.Collections.Generic;
using Feedc.Domain.PersonManagement;
using Feedc.Application.Infrastructure;
using Feedc.Infrastructure.Database.Repository;

namespace Feedc.Application.Query.PersonQueries
{
    public class GetPersonsPhonesQuery : Query<List<Person>>
    {
        private IRepository<Person> personRepository = new Repository<Person>();
        public GetPersonsPhonesQuery()
        {

        }

        public override async Task<QueryExecutionResult<List<Person>>> ExecuteAsync()
        {
            var persons = personRepository.GetAll()
                             .Where(person => !string.IsNullOrWhiteSpace(person.Phone))
                             .ToList();

            return await OkAsync(persons);
        }
    }
}
