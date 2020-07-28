using System;
using System.Threading.Tasks;

namespace Feedc.Application.Infrastructure
{
    public class CommandExecutor
    {
        public IServiceProvider _serviceProvider;

        public CommandExecutor(IServiceProvider serviceProvider)
        {
            _serviceProvider = serviceProvider;
        }

        public async Task<CommandExecutionResult> ExecuteAsync(Command command)
        {
            try
            {
                command.Resolve(_serviceProvider);
                return await command.ExecuteAsync();
            }
            catch (Exception exception)
            {
                return new CommandExecutionResult
                {
                    Exception = exception,
                    Success = false
                };
            }

        }
    }
}
