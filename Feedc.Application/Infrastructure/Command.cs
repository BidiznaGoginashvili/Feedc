using System.Threading.Tasks;

namespace Feedc.Application.Infrastructure
{
    public abstract class Command : ApplicationBase
    {
        public abstract Task<CommandExecutionResult> ExecuteAsync();

        protected Task<CommandExecutionResult> FailAsync()
        {
            var result = new CommandExecutionResult
            {
                Success = false
            };

            return Task.FromResult(result);
        }

        protected Task<CommandExecutionResult> OkAsync()
        {
            var result = new CommandExecutionResult
            {
                Success = true
            };

            return Task.FromResult(result);
        }
    }
}
