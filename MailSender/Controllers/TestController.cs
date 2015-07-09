using System.Web.Mvc;
using MailSender.DTO;
using MailSender.Services;

namespace MailSender.WebUI.Controllers
{
	/// <summary>
	/// The Controller to work with Test.
	/// </summary>
	public class TestController : Controller
	{
		private readonly ITestService testService;

		/// <summary>
		/// Initializes a new instance of the <see cref="TestController"/> class.
		/// </summary>
		/// <param name="testService">The test service.</param>
		public TestController(ITestService testService)
		{
			this.testService = testService;
		}

		/// <summary>
		/// Renders the Test View.
		/// </summary>
		/// <returns>Rendered index view.</returns>
		public ActionResult Index()
		{
			return View();
		}

		/// <summary>
		/// Finds all Tests.
		/// </summary>
		[HttpPost]
		public void FindAll()
		{
			testService.FindAll();
		}

		/// <summary>
		/// Updates this instance.
		/// </summary>
		/// <param name="data">The data.</param>
		[HttpPost]
		public void Update(TestData data)
		{
			testService.Update(data);
		}

		/// <summary>
		/// Creates this instance.
		/// </summary>
		/// <param name="name">The name.</param>
		[HttpPost]
		public void Create(string name)
		{
			testService.Create(name);
		}
	}
}