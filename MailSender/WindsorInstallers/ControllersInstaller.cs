using System.Web.Mvc;
using Castle.MicroKernel.Registration;
using Castle.MicroKernel.SubSystems.Configuration;
using Castle.Windsor;

namespace MailSender.WebUI.WindsorInstallers
{
	/// <summary>
	/// Castle Windsor installer to install components in the container.
	/// </summary>
	public class ControllersInstaller : IWindsorInstaller
	{
		/// <summary>
		/// Performs the registration of the components
		/// <see cref="T:Castle.Windsor.IWindsorContainer" />.
		/// </summary>
		/// <param name="container">The container.</param>
		/// <param name="store">The configuration store.</param>
		public void Install(IWindsorContainer container, IConfigurationStore store)
		{
			container.Register(Classes.FromThisAssembly().BasedOn<IController>().LifestyleTransient());
		}
	}
}