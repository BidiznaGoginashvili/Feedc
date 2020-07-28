namespace Feedc.Application.Infrastructure
{
    public class QueryExecutionResult<T> : ExecutionResult
    {
        public T Data { get; set; }
    }
}
