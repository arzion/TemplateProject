using System;
using System.Web.Mvc;
using System.Web.Routing;
using MailSender.Common;

namespace MailSender.WebUI.Controllers
{
	/// <summary>
	/// The controller factory that is used by application.
	/// </summary>
	public class ControllerFactory : DefaultControllerFactory
	{
		/// <summary>
		/// Retrieves the controller instance for the specified request context and controller type.
		/// </summary>
		/// <param name="requestContext">The context of the HTTP request, which includes the HTTP context and route data.</param>
		/// <param name="controllerType">The type of the controller.</param>
		/// <returns>
		/// The controller instance.
		/// </returns>
		protected override IController GetControllerInstance(RequestContext requestContext, Type controllerType)
		{
			if (controllerType == null)
			{
				return base.GetControllerInstance(requestContext, null);
			}
			return (IController)ComponentLocator.GetComponent(controllerType);
		}
	}
}