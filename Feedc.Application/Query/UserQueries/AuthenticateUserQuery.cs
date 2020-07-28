using System.Threading.Tasks;
using Feedc.Domain.UserManagement;
using Microsoft.AspNetCore.Identity;
using Feedc.Application.Infrastructure;

namespace Feedc.Application.Query.UserQueries
{
    public class AuthenticateUserQuery : Query<User>
    {
        public string Password { get; set; }
        public string FirstName { get; set; }
        public AuthenticateUserQuery()
        {

        }

        public AuthenticateUserQuery(string firstName, string password)
        {
            FirstName = firstName;
            Password = password;
        }

        public override async Task<QueryExecutionResult<User>> ExecuteAsync()
        {
            var userManager = GetService<UserManager<User>>();
            var user = await userManager.FindByNameAsync(FirstName);

            if (user == null)
                return await FailAsync();

            var password = await userManager.CheckPasswordAsync(user, Password);
            if (password)
                return await OkAsync(user);

            return await FailAsync();
        }
    }
}
