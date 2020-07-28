using System;
using System.Threading.Tasks;

namespace Feedc.Application.Infrastructure
{
    public class QueryExecutor
    {
        public IServiceProvider _serviceProvider;

        public QueryExecutor(IServiceProvider serviceProvider)
        {
            _serviceProvider = serviceProvider;
        }

        public async Task<QueryExecutionResult<TResult>> ExecuteAsync<TQuery, TResult>(TQuery query)
            where TQuery : Query<TResult> where TResult : class
        {
            try
            {
                query.Resolve(_serviceProvider);
                return await query.ExecuteAsync();
            }
            catch (Exception ex)
            {
                return new QueryExecutionResult<TResult>
                {
                    Success = false
                };
            }
        }
    }
}
