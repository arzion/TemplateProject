using Castle.MicroKernel.Registration;
using Castle.MicroKernel.SubSystems.Configuration;
using Castle.Windsor;
using Castle.Windsor.Installer;

namespace MailSender.Services.WindsorInstallers
{
	/// <summary>
	/// Castle Windsor installer to install components in the container.
	/// </summary>
	public class ServicesInstaller : IWindsorInstaller
	{
		/// <summary>
		/// Registers types
		/// <see cref="T:Castle.Windsor.IWindsorContainer" />.
		/// </summary>
		/// <param name="container">The container.</param>
		/// <param name="store">The configuration store.</param>
		public void Install(IWindsorContainer container, IConfigurationStore store)
		{
			container.Install(FromAssembly.Named("MailSender.DataAccess"));
			container.Register(Classes.FromThisAssembly()
				.Where(type => type.Namespace != null
					&& (type.Name.Contains("Service")
					&& type.GetInterfaces().Length > 0))
				.WithServiceFirstInterface()
				.LifestyleTransient());
		}
	}
}