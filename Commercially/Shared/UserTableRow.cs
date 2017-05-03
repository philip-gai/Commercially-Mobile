using System;
namespace Commercially
{
	public class UserTableRow
	{
		public readonly User User;

		public UserTableRow(User user)
		{
			User = user;
		}

		public bool LastFirstNameLabelIsHidden {
			get {
				return string.IsNullOrWhiteSpace(User.firstname) || string.IsNullOrWhiteSpace(User.lastname);
			}
		}

		public string LastFirstNameText {
			get {
				if (!LastFirstNameLabelIsHidden) {
					return User.lastname + ", " + User.firstname;
				}
				return null;
			}
		}

		public string EmailText {
			get {
				return User.username;
			}
		}
	}
}
