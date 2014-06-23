using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Http;
using System.Web.Routing;

namespace watchdog.web.app
{
    public class WebApiApplication : System.Web.HttpApplication
    {
        protected void Application_Start()
        {
            GlobalConfiguration.Configure(WebApiConfig.Register);
			RegisterRoutes(RouteTable.Routes);
        }

	    private static void RegisterRoutes(RouteCollection routes)
	    {
	    }
    }
}
