// Created by Philip Gai

namespace Commercially
{
	/// <summary>
	/// User table row manager.
	/// </summary>
	public class UserTableRowManager
	{
		/// <summary>
		/// The user.
		/// </summary>
		public readonly User User;

		/// <summary>
		/// Initializes a new instance of the <see cref="T:Commercially.UserTableRowManager"/> class.
		/// </summary>
		/// <param name="user">User.</param>
		public UserTableRowManager(User user)
		{
			User = user;
		}

		/// <summary>
		/// Gets a value indicating whether this <see cref="T:Commercially.UserTableRowManager"/> name label is hidden.
		/// </summary>
		/// <value><c>true</c> if name label is hidden; otherwise, <c>false</c>.</value>
		public bool NameLabelIsHidden {
			get {
				return string.IsNullOrWhiteSpace(User.firstname) || string.IsNullOrWhiteSpace(User.lastname);
			}
		}

		/// <summary>
		/// Gets the name text.
		/// </summary>
		/// <value>The name text.</value>
		public string NameText {
			get {
				if (!NameLabelIsHidden) {
					return User.lastname + ", " + User.firstname;
				}
				return null;
			}
		}

		/// <summary>
		/// Gets the email text.
		/// </summary>
		/// <value>The email text.</value>
		public string EmailText {
			get {
				return User.username;
			}
		}
	}
}
