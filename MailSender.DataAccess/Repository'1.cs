using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using MailSender.DomainModel;

namespace MailSender.DataAccess
{
	/// <summary>
	/// The repository to work with <see cref="IDomainEntity"/>.
	/// </summary>
	/// <typeparam name="TEntity">The type of the entity.</typeparam>
	public class Repository<TEntity> : IRepository<TEntity> where TEntity : class, IDomainEntity
	{
		private IDbContext context;

		/// <summary>
		/// Initializes a new instance of the <see cref="Repository{TEntity}"/> class.
		/// </summary>
		/// <param name="dbContext">The db context.</param>
		public Repository(IDbContext dbContext)
		{
			context = dbContext;
		}

		/// <summary>
		/// Finalizes an instance of the <see cref="Repository{TEntity}"/> class.
		/// </summary>
		~Repository()
		{
			if (context != null)
			{
				context.Dispose();
			}
		}

		private IDbSet<TEntity> Entities
		{
			get { return context.Set<TEntity>(); }
		}

		/// <summary>
		/// Finds all entities.
		/// </summary>
		/// <returns>
		/// The list of <see cref="TEntity" />
		/// </returns>
		public IEnumerable<TEntity> FindAll()
		{
			return Entities.AsQueryable();
		}

		/// <summary>
		/// Finds the entity by specified id.
		/// </summary>
		/// <param name="id">The id.</param>
		/// <returns>
		/// The found <see cref="TEntity" />
		/// </returns>
		public virtual TEntity Find(int id)
		{
			return Entities.Find(id);
		}

		/// <summary>
		/// Finds entities by the criteria.
		/// </summary>
		/// <param name="criteria">The criteria.</param>
		/// <returns>
		/// The list of <see cref="TEntity" /> found by criteria.
		/// </returns>
		public IEnumerable<TEntity> ByCriteria(Func<TEntity, bool> criteria)
		{
			return Entities.Where(criteria);
		}

		/// <summary>
		/// Adds the specified entity.
		/// </summary>
		/// <param name="entity">The entity.</param>
		public virtual void Add(TEntity entity)
		{
			Entities.Add(entity);
		}

		/// <summary>
		/// Deletes the specified entity.
		/// </summary>
		/// <param name="entity">The entity.</param>
		public void Delete(TEntity entity)
		{
			Entities.Remove(entity);
		}

		/// <summary>
		/// Deletes the entity by specified id.
		/// </summary>
		/// <param name="id">The id.</param>
		public virtual void Delete(int id)
		{
			Entities.Remove(context.Set<TEntity>().Find(id));
		}

		/// <summary>
		/// Saves this instance.
		/// </summary>
		public void Save()
		{
			context.Save();
		}

		/// <summary>
		/// Performs application-defined tasks associated with freeing, releasing, or resetting unmanaged resources.
		/// </summary>
		public void Dispose()
		{
			if (context != null)
			{
				context.Dispose();
				context = null;
				GC.SuppressFinalize(this);
			}
		}
	}
}