using System.Linq;
using System.Threading.Tasks;
using Feedc.Domain.PersonManagement;
using Feedc.Application.Infrastructure;
using Feedc.Infrastructure.Database.Repository;

namespace Feedc.Application.Query.PersonQueries
{
    public class GetPersonQuery : Query<Person>
    {
        public string Phone { get; set; }

        public GetPersonQuery()
        {

        }

        public GetPersonQuery(string phone)
        {
            Phone = phone;
        }

        public override async Task<QueryExecutionResult<Person>> ExecuteAsync()
        {
            var repository = GetService<IRepository<Person>>();
            var person = repository
                  .GetAll()
                  .FirstOrDefault(person => person.Phone == Phone);

            if (person == null)
                return await FailAsync();

            return await OkAsync(person);
        }
    }
}
