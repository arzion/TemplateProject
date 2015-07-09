using Castle.MicroKernel.Registration;
using Castle.MicroKernel.SubSystems.Configuration;
using Castle.Windsor;

namespace MailSender.DataAccess.WindsorInstallers
{
	/// <summary>
	/// The installer of DataAccess dependencies.
	/// </summary>
	public class DataAccessInstaller : IWindsorInstaller
	{
		/// <summary>
		/// Performs the installation in the <see cref="T:Castle.Windsor.IWindsorContainer" />.
		/// </summary>
		/// <param name="container">The container.</param>
		/// <param name="store">The configuration store.</param>
		public void Install(IWindsorContainer container, IConfigurationStore store)
		{
			container.Register(
				Component.For(typeof(IRepository<>))
				.ImplementedBy(typeof(Repository<>))
				.LifestyleTransient());

			container.Register(
				Component.For<IDbContext>()
				.ImplementedBy<MailSenderDbContext>()
				.LifestyleTransient());
		}
	}
}