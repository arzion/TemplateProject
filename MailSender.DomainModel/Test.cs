namespace MailSender.DomainModel
{
	/// <summary>
	/// The Test entity.
	/// </summary>
	public class Test : IDomainEntity
	{
		private string name;

		/// <summary>
		/// Gets or sets the identifier of the <see cref="Test"/>.
		/// </summary>
		public virtual int Id { get; protected internal set; }

		/// <summary>
		/// The name of the <see cref="Test"/>.
		/// </summary>
		public virtual string Name
		{
			get { return name; }
			set { name = value; }
		}

		/// <summary>
		/// Initializes a new instance of the <see cref="Test"/> class.
		/// </summary>
		/// <param name="name">The name.</param>
		public Test(string name)
		{
			this.name = name;
		}
	}
}