using System.Web.Mvc;
using System.Web.Routing;
using Combres;
using MailSender.WebUI.Utils.Localization;

namespace MailSender.WebUI
{
	/// <summary>
	/// The configuration of the Routes.
	/// </summary>
	public class RouteConfig
	{
		/// <summary>
		/// Registers the routes.
		/// </summary>
		/// <param name="routes">The routes.</param>
		public static void RegisterRoutes(RouteCollection routes)
		{
			routes.AddCombresRoute("Combres");
			routes.IgnoreRoute("{resource}.axd/{*pathInfo}");

			routes.MapLocalizeRoute(
				name: "DefaultShortCUltureSpecific",
				url: "{culture}/{controller}/{id}",
				defaults: new { controller = "Home", action = "Index", id = UrlParameter.Optional },
				constraints: new { culture = LocaleHelper.SupportedCulturesRegex, id = @"\d+" });

			routes.MapRouteToLocalizeRedirect(
				name: "DefaultShort",
				url: "{controller}/{id}",
				defaults: new { controller = "Home", action = "Index", id = UrlParameter.Optional },
				constraints: new { id = @"\d+" });

			routes.MapLocalizeRoute(
				name: "DefaultCultureSpecific",
				url: "{culture}/{controller}/{action}/{id}",
				defaults: new { controller = "Home", action = "Index", id = UrlParameter.Optional },
				constraints: new { culture = LocaleHelper.SupportedCulturesRegex });

			routes.MapRouteToLocalizeRedirect(
				name: "Default",
				url: "{controller}/{action}/{id}",
				defaults: new { controller = "Home", action = "Index", id = UrlParameter.Optional });
		}
	}
}