using System.Threading.Tasks;
using Feedc.Domain.UserManagement;
using Microsoft.AspNetCore.Identity;
using Feedc.Application.Infrastructure;

namespace Feedc.Application.UserCommands.Command
{
    public class CreateUserCommand : Infrastructure.Command
    {
        public string Password { get; set; }
        public string FirstName { get; set; }

        public CreateUserCommand()
        {

        }

        public override async Task<CommandExecutionResult> ExecuteAsync()
        {
            var userManager = GetService<UserManager<User>>();

            var user = new User(FirstName, Password);
            user.UserName = FirstName;

            var result = await userManager.CreateAsync(user, Password);

            if (!result.Succeeded)
                return await FailAsync();

            return await OkAsync();
        }
    }
}
