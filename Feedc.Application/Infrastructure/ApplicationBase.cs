using Serilog;
using System;

namespace Feedc.Application.Infrastructure
{
    public abstract class ApplicationBase
    {
        protected ILogger _logger;
        protected IServiceProvider _serviceProvider;

        public void Resolve(ILogger logger, IServiceProvider serviceProvider)
        {
            _logger = logger;
            _serviceProvider = serviceProvider;
        }

        public T GetService<T>() => (T)_serviceProvider.GetService(typeof(T));
    }
}
