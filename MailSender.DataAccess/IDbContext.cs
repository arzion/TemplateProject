using System;
using System.Data.Entity;
using MailSender.DomainModel;

namespace MailSender.DataAccess
{
	/// <summary>
	/// The DbContext.
	/// </summary>
	public interface IDbContext : IDisposable
	{
		/// <summary>
		/// Gets the Set of the instance.
		/// </summary>
		/// <typeparam name="TEntity">The type of the entity.</typeparam>
		/// <returns>The DbSet of <see cref="TEntity"/></returns>
		IDbSet<TEntity> Set<TEntity>() where TEntity : class, IDomainEntity;

		/// <summary>
		/// Saves the changes associated to the context.
		/// </summary>
		/// <returns>The result identifier.</returns>
		int Save();
	}
}