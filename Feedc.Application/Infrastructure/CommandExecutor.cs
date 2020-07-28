using System;
using Serilog;
using System.Threading.Tasks;

namespace Feedc.Application.Infrastructure
{
    public class CommandExecutor
    {
        private readonly ILogger _logger;
        public IServiceProvider _serviceProvider;

        public CommandExecutor(ILogger logger, IServiceProvider serviceProvider)
        {
            _logger = Log.ForContext<CommandExecutor>();
            _serviceProvider = serviceProvider;
        }

        public async Task<CommandExecutionResult> ExecuteAsync(Command command)
        {
            try
            {
                command.Resolve(_logger, _serviceProvider);
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
