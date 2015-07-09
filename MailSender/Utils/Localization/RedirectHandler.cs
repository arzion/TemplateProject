using System.Diagnostics.CodeAnalysis;
using System.Web;

namespace MailSender.WebUI.Utils.Localization
{
	/// <summary>
	/// The handler for redirection to the URL.
	/// </summary>
	public class RedirectHandler : IHttpHandler
	{
		private readonly string newUrl;

		/// <summary>
		/// Initializes a new instance of the <see cref="RedirectHandler"/> class.
		/// </summary>
		/// <param name="newUrl">The new URL to redirect.</param>
		[SuppressMessage(category: "Microsoft.Design", checkId: "CA1054:UriParametersShouldNotBeStrings",
			Justification = "We just use string since HttpResponse.Redirect only accept as string parameter.")]
		public RedirectHandler(string newUrl)
		{
			this.newUrl = newUrl;
		}

		/// <summary>
		/// Gets a value indicating whether another request can use the <see cref="T:System.Web.IHttpHandler" /> instance.
		/// </summary>
		/// <returns>true if the <see cref="T:System.Web.IHttpHandler" /> instance is reusable; otherwise, false.</returns>
		public bool IsReusable
		{
			get { return true; }
		}

		/// <summary>
		/// Enables processing of HTTP Web requests by a custom HttpHandler that implements the <see cref="T:System.Web.IHttpHandler" /> interface.
		/// </summary>
		/// <param name="context">An <see cref="T:System.Web.HttpContext" /> object that provides references to the intrinsic server objects (for example, Request, Response, Session, and Server) used to service HTTP requests.</param>
		public void ProcessRequest(HttpContext context)
		{
			context.Response.Redirect(newUrl);
		}
	}
}