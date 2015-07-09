using System;
using System.Collections.Generic;
using MailSender.DomainModel;

namespace MailSender.DataAccess
{
	/// <summary>
	/// The repository to work with <see cref="IDomainEntity"/>.
	/// </summary>
	/// <typeparam name="TEntity">The type of the entity.</typeparam>
	public interface IRepository<TEntity> : IDisposable where TEntity : class, IDomainEntity
	{
		/// <summary>
		/// Finds all entities.
		/// </summary>
		/// <returns>The list of <see cref="TEntity"/></returns>
		IEnumerable<TEntity> FindAll();

		/// <summary>
		/// Finds the entity by specified id.
		/// </summary>
		/// <param name="id">The id.</param>
		/// <returns>The found <see cref="TEntity"/></returns>
		TEntity Find(int id);

		/// <summary>
		/// Finds entities by the criteria.
		/// </summary>
		/// <param name="criteria">The criteria.</param>
		/// <returns>The list of <see cref="TEntity"/> found by criteria.</returns>
		IEnumerable<TEntity> ByCriteria(Func<TEntity, bool> criteria);

		/// <summary>
		/// Adds the specified entity.
		/// </summary>
		/// <param name="entity">The entity.</param>
		void Add(TEntity entity);

		/// <summary>
		/// Deletes the specified entity.
		/// </summary>
		/// <param name="entity">The entity.</param>
		void Delete(TEntity entity);

		/// <summary>
		/// Deletes the entity by specified id.
		/// </summary>
		/// <param name="id">The id.</param>
		void Delete(int id);

		/// <summary>
		/// Saves this instance.
		/// </summary>
		void Save();
	}
}