using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.Http;

using DryIoc;
using watchdog.data.context;
using watchdog.web.app.Controllers;
using watchdog.web.app.Resolvers;

namespace watchdog.web.app
{
	public static class WebApiConfig
	{
		public static void Register(HttpConfiguration config)
		{
			var resolver = new DryIOCResolver(c =>
			{
				c.RegisterDelegate<IDbContext>(x => new DbContext("watchdogDb"));

				c.Register<MachinesController, MachinesController>();
			});
			config.DependencyResolver = resolver;

			// Web API configuration and services

			// Web API routes
			config.MapHttpAttributeRoutes();

			config.Routes.MapHttpRoute(
				name: "DefaultApi",
				routeTemplate: "api/{controller}/{id}",
				defaults: new { id = RouteParameter.Optional }
			);
		}
	}
}
