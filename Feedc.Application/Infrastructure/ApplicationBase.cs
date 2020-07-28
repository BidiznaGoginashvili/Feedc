using System;

namespace Feedc.Application.Infrastructure
{
    public abstract class ApplicationBase
    {
        protected IServiceProvider _serviceProvider;

        public void Resolve(IServiceProvider serviceProvider)
        {
            _serviceProvider = serviceProvider;
        }

        public T GetService<T>() => (T)_serviceProvider.GetService(typeof(T));
    }
}
