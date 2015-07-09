using System.Web.Routing;

namespace MailSender.WebUI.Utils.Localization
{
	/// <summary>
	/// The extensions for Route Collection.
	/// </summary>
	public static class RouteCollectionExtensions
	{
		/// <summary>
		/// Maps the route to localize redirect.
		/// </summary>
		/// <param name="routes">The routes.</param>
		/// <param name="name">The name.</param>
		/// <param name="url">The URL.</param>
		/// <param name="defaults">The defaults.</param>
		/// <param name="constraints">The constraints.</param>
		/// <returns>The mapped route to localization redirect.</returns>
		public static Route MapRouteToLocalizeRedirect(
			this RouteCollection routes,
			string name,
			string url,
			object defaults,
			object constraints = null)
		{
			var redirectRoute = new Route(
				url,
				new RouteValueDictionary(defaults),
				new RouteValueDictionary(constraints ?? new { }),
				new LocalizationRedirectRouteHandler());
			routes.Add(name, redirectRoute);

			return redirectRoute;
		}

		/// <summary>
		/// Maps the localized route.
		/// </summary>
		/// <param name="routes">The routes.</param>
		/// <param name="name">The name.</param>
		/// <param name="url">The URL.</param>
		/// <param name="defaults">The defaults.</param>
		/// <returns>Mapped localization route.</returns>
		public static Route MapLocalizeRoute(
			this RouteCollection routes,
			string name,
			string url,
			object defaults)
		{
			return routes.MapLocalizeRoute(name, url, defaults, new { });
		}

		/// <summary>
		/// Maps the localize route.
		/// </summary>
		/// <param name="routes">The routes.</param>
		/// <param name="name">The name.</param>
		/// <param name="url">The URL.</param>
		/// <param name="defaults">The defaults.</param>
		/// <param name="constraints">The constraints.</param>
		/// <returns>Mapped localized route.</returns>
		public static Route MapLocalizeRoute(
			this RouteCollection routes,
			string name,
			string url,
			object defaults,
			object constraints)
		{
			var route = new Route(
				url,
				new RouteValueDictionary(defaults),
				new RouteValueDictionary(constraints),
				new LocalizedRouteHandler());

			routes.Add(name, route);

			return route;
		}
	}
}