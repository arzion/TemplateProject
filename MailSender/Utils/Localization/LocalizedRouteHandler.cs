using System.Globalization;
using System.Threading;
using System.Web;
using System.Web.Mvc;
using System.Web.Routing;

namespace MailSender.WebUI.Utils.Localization
{
	/// <summary>
	/// MVC route handler for localized requests.
	/// </summary>
	public class LocalizedRouteHandler : MvcRouteHandler
	{
		/// <summary>
		/// Returns the HTTP handler by using the specified HTTP context.
		/// </summary>
		/// <param name="requestContext">The request context.</param>
		/// <returns>
		/// The HTTP handler.
		/// </returns>
		protected override IHttpHandler GetHttpHandler(RequestContext requestContext)
		{
			var cookieLocale = requestContext.HttpContext.Request.Cookies[LocaleHelper.LocaleCookieKey];
			var localeFromCookie = cookieLocale != null ? cookieLocale.Value.ToLower() : string.Empty;
			var localeFromRoute = requestContext.RouteData.Values["culture"].ToString().ToLower();

			// if both are not supported - redirect to default locale
			if (!LocaleHelper.IsCultureSupported(localeFromCookie)
				&& !LocaleHelper.IsCultureSupported(localeFromRoute))
			{
				return GetRedirectHandlerWithDefaults(requestContext);
			}

			// if only route locale is not supported - redirect to cookie locale
			if (!LocaleHelper.IsCultureSupported(localeFromRoute))
			{
				return GetRedirectHandlerWithCultureRoute(requestContext, localeFromCookie);
			}

			// if only cookie locale is not supported - change the cookie
			if (!LocaleHelper.IsCultureSupported(localeFromCookie))
			{
				SetLocaleCookie(requestContext, localeFromRoute);
				localeFromCookie = localeFromRoute;
			}

			// then both of locales are supported but if they different redirect to cookie locale
			if (localeFromCookie != localeFromRoute)
			{
				return GetRedirectHandlerWithCultureRoute(requestContext, localeFromCookie);
			}

			// set culture for thread as cookie locale
			try
			{
				var culture = CultureInfo.GetCultureInfo(localeFromCookie);
				Thread.CurrentThread.CurrentCulture = culture;
				Thread.CurrentThread.CurrentUICulture = culture;
			}
			catch (CultureNotFoundException)
			{
				return GetRedirectHandlerWithDefaults(requestContext);
			}

			return base.GetHttpHandler(requestContext);
		}

		#region helpers

		private static IHttpHandler GetRedirectHandlerWithCultureRoute(RequestContext requestContext, string cultureValue)
		{
			var routeValues = requestContext.RouteData.Values;
			routeValues["culture"] = cultureValue;
			return new RedirectHandler(new UrlHelper(requestContext).RouteUrl(routeValues));
		}

		private static void SetLocaleCookie(RequestContext requestContext, string value)
		{
			requestContext.HttpContext.Response.AppendCookie(new HttpCookie(LocaleHelper.LocaleCookieKey, value));
		}

		private static IHttpHandler GetRedirectHandlerWithDefaults(RequestContext requestContext)
		{
			var routeValues = requestContext.RouteData.Values;
			routeValues["culture"] = LocaleHelper.DefaultCulture;
			SetLocaleCookie(requestContext, LocaleHelper.DefaultCulture);
			return new RedirectHandler(new UrlHelper(requestContext).RouteUrl(routeValues));
		}

		#endregion
	}
}