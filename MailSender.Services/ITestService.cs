using System.Collections.Generic;
using MailSender.DomainModel;
using MailSender.DTO;

namespace MailSender.Services
{
	/// <summary>
	/// The service to work with test entity.
	/// </summary>
	public interface ITestService
	{
		/// <summary>
		/// Creates the <see cref="Test"/>.
		/// </summary>
		/// <param name="name">The name of the <see cref="Test"/> to create.</param>
		/// <returns>The id of the created <see cref="Test"/></returns>
		int Create(string name);

		/// <summary>
		/// Updates the <see cref="Test"/> with specified data.
		/// </summary>
		/// <param name="data">The data.</param>
		/// <returns>The result of Update operation.</returns>
		bool Update(TestData data);

		/// <summary>
		/// Find all <see cref="Test"/> in the system.
		/// </summary>
		/// <returns>The Dictionary of key and value with Ids and Names of <see cref="Test"/>s.</returns>
		Dictionary<int, string> FindAll();
	}
}