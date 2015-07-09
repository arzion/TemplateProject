using System.Web;
using System.Web.Mvc;
using System.Web.Routing;
using Castle.Windsor.Installer;
using MailSender.Common;
using MailSender.WebUI.Controllers;

namespace MailSender.WebUI
{
	public class MvcApplication : HttpApplication
	{
		/// <summary>
		/// Handle the Application the start.
		/// </summary>
		protected void Application_Start()
		{
			AreaRegistration.RegisterAllAreas();
			RouteConfig.RegisterRoutes(RouteTable.Routes);
			ControllerBuilder.Current.SetControllerFactory(new ControllerFactory());

			BootstrapCastleWindsor();
		}

		/// <summary>
		/// Register castle Windsor components.
		/// </summary>
		private static void BootstrapCastleWindsor()
		{
			var container = ComponentLocator.Container;
			container.Install(
				FromAssembly.Named("MailSender.Services"),
				FromAssembly.This());
		}
	}
}