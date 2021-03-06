﻿using System;
using Serilog;
using System.Threading.Tasks;

namespace Feedc.Application.Infrastructure
{
    public class QueryExecutor
    {
        private readonly ILogger _logger;
        public IServiceProvider _serviceProvider;

        public QueryExecutor(ILogger logger, IServiceProvider serviceProvider)
        {
            _logger = Log.ForContext<CommandExecutor>();
            _serviceProvider = serviceProvider;
        }

        public async Task<QueryExecutionResult<TResult>> ExecuteAsync<TQuery, TResult>(TQuery query)
            where TQuery : Query<TResult> where TResult : class
        {
            try
            {
                query.Resolve(_logger, _serviceProvider);
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
