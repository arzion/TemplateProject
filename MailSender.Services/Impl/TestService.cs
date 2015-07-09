using System;
using System.Collections.Generic;
using System.Linq;
using MailSender.DataAccess;
using MailSender.DomainModel;
using MailSender.DTO;

namespace MailSender.Services.Impl
{
	/// <summary>
	/// The service to work with test entity.
	/// </summary>
	public class TestService : ITestService
	{
		private readonly IRepository<Test> testRepository;

		/// <summary>
		/// Initializes a new instance of the <see cref="TestService"/> class.
		/// </summary>
		/// <param name="testRepository">The test repository.</param>
		public TestService(IRepository<Test> testRepository)
		{
			this.testRepository = testRepository;
		}

		/// <summary>
		/// Creates the <see cref="Test" />.
		/// </summary>
		/// <param name="name">The name of the <see cref="Test" /> to create.</param>
		/// <returns> The id of the created <see cref="Test" /> </returns>
		public int Create(string name)
		{
			var test = new Test(name);
			try
			{
				testRepository.Add(test);
				testRepository.Save();
				return test.Id;
			}
			catch (Exception)
			{
				return -1;
			}
		}

		/// <summary>
		/// Updates the <see cref="Test" /> with specified data.
		/// </summary>
		/// <param name="data">The data.</param>
		/// <returns>
		/// The result of Update operation.
		/// </returns>
		/// <exception cref="InvalidOperationException">Id is not valid.</exception>
		public bool Update(TestData data)
		{
			var test = testRepository.Find(data.Id);
			if (test == null)
			{
				throw new InvalidOperationException("Id is not valid");
			}
			test.Name = data.Name;
			try
			{
				testRepository.Save();
				return true;
			}
			catch (Exception)
			{
				return false;
			}
		}

		/// <summary>
		/// Find all <see cref="Test" /> in the system.
		/// </summary>
		/// <returns>The Dictionary of key and value with Ids and Names of <see cref="Test" />s.</returns>
		public Dictionary<int, string> FindAll()
		{
			return testRepository.FindAll().ToDictionary(t => t.Id, t => t.Name);
		}
	}
}