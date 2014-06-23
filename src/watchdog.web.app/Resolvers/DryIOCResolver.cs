using System;
using System.Collections.Generic;
using System.Web.Http.Dependencies;

using DryIoc;

namespace watchdog.web.app.Resolvers
{
	public class DryIOCResolver : IDependencyResolver
	{
		private readonly Container _container;
		private readonly Action<Container> _configuration;

		public DryIOCResolver(Action<Container> configuration)
		{
			_configuration = configuration;
		}

		private DryIOCResolver(Container container)
		{
			_container = container;
		}

		public void Dispose()
		{
			_container.Dispose();
		}

		public object GetService(Type serviceType)
		{
			if (_container == null) return null;
			try
			{
				return _container.Resolve(serviceType);
			}
			catch (Exception)
			{
				//TODO: implement error log here
				return null;
			}
		}

		public IEnumerable<object> GetServices(Type serviceType)
		{
			if (_container == null) return new Object[0];
			try
			{
				return (IEnumerable<object>)_container.Resolve(typeof(IEnumerable<>).MakeGenericType(serviceType));
			}
			catch (Exception)
			{
				//TODO: implement error log here
				return new Object[0];
			}
		}

		public IDependencyScope BeginScope()
		{
			var container = new Container();
			_configuration(container);
			return new DryIOCResolver(container);
		}
	}
}
