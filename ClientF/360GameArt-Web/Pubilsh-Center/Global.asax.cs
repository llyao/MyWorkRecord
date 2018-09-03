using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Optimization;
using System.Web.Routing;
using ClientApp.Runtime;
using ClientCore.DB;
using ClientCore.Runtime;

namespace WebApp
{
	public class MvcApplication : System.Web.HttpApplication
	{
		private static App app;
		protected void Application_Start()
		{
			AreaRegistration.RegisterAllAreas();
			FilterConfig.RegisterGlobalFilters(GlobalFilters.Filters);
			RouteConfig.RegisterRoutes(RouteTable.Routes);
			BundleConfig.RegisterBundles(BundleTable.Bundles);



			app = new DotNetServer();
			app.Awake();

		}
	}
}
