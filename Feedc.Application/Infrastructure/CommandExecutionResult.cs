using System;

namespace Feedc.Application.Infrastructure
{
    public class CommandExecutionResult : ExecutionResult
    {
        public Exception Exception { get; set; }
    }
}
