using System;
using System.IO;
using System.Web;

namespace watchdog.web.app.HttpModules
{
	public class SPAModule : IHttpModule
	{
		/// <summary>
		/// You will need to configure this module in the Web.config file of your
		/// web and register it with IIS before being able to use it. For more information
		/// see the following link: http://go.microsoft.com/?linkid=8101007
		/// </summary>
		#region IHttpModule Members

		public void Dispose()
		{
			//clean-up code here.
		}

		public void Init(HttpApplication context)
		{
			// Below is an example of how you can handle LogRequest event and provide 
			// custom logging implementation for it
			context.LogRequest += OnLogRequest;

			context.BeginRequest += ContextOnBeginRequest;
		}

		private void ContextOnBeginRequest(object sender, EventArgs eventArgs)
		{
			var context = HttpContext.Current;
			var virtualPath = context.Request.Path;
			var physicalPath = context.Server.MapPath(virtualPath);
			if (File.Exists(physicalPath)) return;
			if (virtualPath.ToLower().EndsWith("index.html")) return;
			if (virtualPath.ToLower().StartsWith("/api/")) return;
			context.RewritePath("~/index.html");
		}

		#endregion

		public void OnLogRequest(Object source, EventArgs e)
		{
			//custom logging logic can go here
		}
	}
}
