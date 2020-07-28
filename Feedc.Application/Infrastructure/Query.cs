using System.Threading.Tasks;

namespace Feedc.Application.Infrastructure
{
    public abstract class Query<TResult> : ApplicationBase
        where TResult : class
    {
        public abstract Task<QueryExecutionResult<TResult>> ExecuteAsync();

        protected Task<QueryExecutionResult<TResult>> FailAsync()
        {
            var result = new QueryExecutionResult<TResult>
            {
                Success = false
            };

            return Task.FromResult(result);
        }

        protected Task<QueryExecutionResult<TResult>> OkAsync(TResult data)
        {
            var result = new QueryExecutionResult<TResult>
            {
                Success = true,
                Data = data
            };

            return Task.FromResult(result);
        }
    }
}
