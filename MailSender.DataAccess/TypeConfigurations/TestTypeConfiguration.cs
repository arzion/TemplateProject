using System.Data.Entity.ModelConfiguration;
using MailSender.DomainModel;

namespace MailSender.DataAccess.TypeConfigurations
{
	/// <summary>
	/// The fluent configuration of <see cref="Test"/> type.
	/// </summary>
	internal class TestTypeConfiguration : EntityTypeConfiguration<Test>
	{
		/// <summary>
		/// Initializes a new instance of the <see cref="TestTypeConfiguration"/> class.
		/// </summary>
		public TestTypeConfiguration()
		{
			ToTable("Tests");
			HasKey(game => game.Id);
			Property(game => game.Name).HasMaxLength(50).IsRequired();
		}
	}
}