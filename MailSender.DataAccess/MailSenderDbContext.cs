using System;
using System.Data.Entity;
using System.Data.Entity.ModelConfiguration;
using System.Linq;
using System.Reflection;
using MailSender.DomainModel;

namespace MailSender.DataAccess
{
	/// <summary>
	/// The context of the Scrabble.
	/// </summary>
	public class MailSenderDbContext : DbContext, IDbContext
	{
		/// <summary>
		/// Initializes static members of the <see cref="MailSenderDbContext"/> class.
		/// </summary>
		static MailSenderDbContext()
		{
			Database.SetInitializer(new CreateDatabaseIfNotExists<MailSenderDbContext>());
		}

		/// <summary>
		/// Gets the Set of the instance.
		/// </summary>
		/// <typeparam name="TEntity">The type of the entity.</typeparam>
		/// <returns>
		/// The DbSet of <see cref="TEntity" />
		/// </returns>
		public new IDbSet<TEntity> Set<TEntity>() where TEntity : class, IDomainEntity
		{
			return base.Set<TEntity>();
		}

		/// <summary>
		/// This method is called when the model for a derived context has been initialized, but
		/// before the model has been locked down and used to initialize the context.  The default
		/// implementation of this method does nothing, but it can be overridden in a derived class
		/// such that the model can be further configured before it is locked down.
		/// </summary>
		/// <param name="modelBuilder">The builder that defines the model for the context being created.</param>
		/// <remarks>
		/// Typically, this method is called only once when the first instance of a derived context
		/// is created.  The model for that context is then cached and is for all further instances of
		/// the context in the app domain.  This caching can be disabled by setting the ModelCaching
		/// property on the given ModelBuidler, but note that this can seriously degrade performance.
		/// More control over caching is provided through use of the DbModelBuilder and DbContextFactory
		/// classes directly.
		/// </remarks>
		protected override void OnModelCreating(DbModelBuilder modelBuilder)
		{
			var typesToRegister = Assembly.GetExecutingAssembly().GetTypes()
				.Where(type => type.Namespace != null && (type.BaseType != null
					&& type.BaseType.IsGenericType
					&& !string.IsNullOrEmpty(type.Namespace)
					&& type.Namespace.Contains("TypeConfiguration")
					&& type.BaseType.GetGenericTypeDefinition() == typeof(EntityTypeConfiguration<>)));

			foreach (var type in typesToRegister)
			{
				dynamic configurationInstance = Activator.CreateInstance(type);
				modelBuilder.Configurations.Add(configurationInstance);
			}

			base.OnModelCreating(modelBuilder);
		}

		/// <summary>
		/// Commits the changes associated with context.
		/// </summary>
		/// <returns>
		/// The result identifier.
		/// </returns>
		public int Save()
		{
			return SaveChanges();
		}
	}
}