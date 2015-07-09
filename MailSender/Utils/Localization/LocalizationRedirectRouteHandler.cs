using System.Web;
using System.Web.Mvc;
using System.Web.Routing;

namespace MailSender.WebUI.Utils.Localization
{
	/// <summary>
	/// The Localization Route Handler. Return redirect handler with route locale from cookies.
	/// If there is no cookies - just open target page with default locale.
	/// </summary>
	public class LocalizationRedirectRouteHandler : IRouteHandler
	{
		/// <summary>
		/// Provides the object that processes the request.
		/// </summary>
		/// <param name="requestContext">An object that encapsulates information about the request.</param>
		/// <returns>
		/// An object that processes the request.
		/// </returns>
		public IHttpHandler GetHttpHandler(RequestContext requestContext)
		{
			var routeValues = requestContext.RouteData.Values;

			var cookieLocale = requestContext.HttpContext.Request.Cookies[LocaleHelper.LocaleCookieKey];
			if (cookieLocale != null && LocaleHelper.IsCultureSupported(cookieLocale.Value))
			{
				routeValues["culture"] = cookieLocale.Value;
				return new RedirectHandler(new UrlHelper(requestContext).RouteUrl(routeValues));
			}
			routeValues["culture"] = LocaleHelper.DefaultCulture;
			return new RedirectHandler(new UrlHelper(requestContext).RouteUrl(routeValues));
		}
	}
}