using System.Linq;
using System.Web;

namespace MailSender.WebUI.Utils.Localization
{
	/// <summary>
	/// The helper to work with application cultures.
	/// </summary>
	public static class LocaleHelper
	{
		/// <summary>
		/// The locale cookie key.
		/// </summary>
		public const string LocaleCookieKey = "l";

		/// <summary>
		/// The default culture.
		/// </summary>
		public const string DefaultCulture = "en";

		/// <summary>
		/// Gets the supported cultures.
		/// </summary>
		public static string[] SupportedCultures
		{
			get { return new[] { "en", "ru", "uk" }; }
		}

		/// <summary>
		/// Gets the supported cultures Regex.
		/// </summary>
		public static string SupportedCulturesRegex
		{
			get
			{
				return string.Join(
					"|",
					SupportedCultures.Select(culture => string.Format(@"\b({0}){{1}}\b", culture)));
			}
		}

		/// <summary>
		/// Determines whether the culture is supported or not.
		/// </summary>
		/// <param name="culture">The culture.</param>
		/// <returns><c>true</c> if culture is supported.</returns>
		public static bool IsCultureSupported(string culture)
		{
			return SupportedCultures.Contains(culture);
		}

		/// <summary>
		/// Gets the route locale.
		/// </summary>
		/// <param name="httpRequest">The HTTP request.</param>
		/// <returns>The locale of the route.</returns>
		public static string GetRouteLocale(HttpRequestBase httpRequest)
		{
			return httpRequest.RequestContext.RouteData.Values["culture"].ToString();
		}
	}
}