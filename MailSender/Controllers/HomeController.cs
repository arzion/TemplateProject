using System.Web.Mvc;

namespace MailSender.WebUI.Controllers
{
	/// <summary>
	/// The Home controller.
	/// </summary>
	public class HomeController : Controller
	{
		/// <summary>
		/// Indexes this instance.
		/// </summary>
		/// <returns>Rendered view.</returns>
		public ActionResult Index()
		{
			return View();
		}
	}
}