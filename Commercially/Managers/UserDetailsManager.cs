// Created by Philip Gai

using System;
using Newtonsoft.Json.Linq;

namespace Commercially
{
	/// <summary>
	/// User details manager.
	/// </summary>
	public class UserDetailsManager
	{
		/// <summary>
		/// The user.
		/// </summary>
		public User User;

		/// <summary>
		/// The user's requests.
		/// </summary>
		public Request[] Requests;

		/// <summary>
		/// The height of the table header.
		/// </summary>
		public const double TableHeaderHeight = 50;

		/// <summary>
		/// The height of the table row.
		/// </summary>
		public const double TableRowHeight = 88;

		/// <summary>
		/// The table row alpha double.
		/// </summary>
		public const double TableRowAlphaDouble = 0.33;

		/// <summary>
		/// The table row alpha byte.
		/// </summary>
		public const byte TableRowAlphaByte = 0x54;

		/// <summary>
		/// The table header title.
		/// </summary>
		public readonly static string TableHeaderTitle = RequestStatusType.Assigned.ToString();

		/// <summary>
		/// The color of the table header.
		/// </summary>
		public readonly static Color TableHeaderColor = GlobalConstants.DefaultColors.Yellow;

		/// <summary>
		/// Gets the name text.
		/// </summary>
		/// <value>The name text.</value>
		public string NameText {
			get {
				return User.firstname + " " + User.lastname;
			}
		}

		/// <summary>
		/// Gets the username text.
		/// </summary>
		/// <value>The username text.</value>
		public string UsernameText {
			get {
				return User.username;
			}
		}

		/// <summary>
		/// Gets the email text.
		/// </summary>
		/// <value>The email text.</value>
		public string EmailText {
			get {
				return User.email;
			}
		}

		/// <summary>
		/// Gets the phone text.
		/// </summary>
		/// <value>The phone text.</value>
		public string PhoneText {
			get {
				return User.phone;
			}
		}

		/// <summary>
		/// Gets a value indicating whether this <see cref="T:Commercially.UserDetailsManager"/> change password button is hidden.
		/// </summary>
		/// <value><c>true</c> if change password button is hidden; otherwise, <c>false</c>.</value>
		public bool ChangePasswordButtonIsHidden {
			get {
				return !User.id.Equals(Session.User.id);
			}
		}

		/// <summary>
		/// Gets if the name is changed.
		/// </summary>
		/// <returns><c>true</c>, if name is changed, <c>false</c> otherwise.</returns>
		/// <param name="name">Name.</param>
		bool NameIsChanged(string name)
		{
			if (string.IsNullOrWhiteSpace(User.firstname) || string.IsNullOrWhiteSpace(User.lastname)) return !string.IsNullOrWhiteSpace(name);
			return !name.Equals(User.firstname + " " + User.lastname);
		}

		/// <summary>
		/// Gets if the username is changed.
		/// </summary>
		/// <returns><c>true</c>, if the username is changed, <c>false</c> otherwise.</returns>
		/// <param name="username">Username.</param>
		bool UsernameIsChanged(string username)
		{
			if (string.IsNullOrWhiteSpace(User.username)) return !string.IsNullOrWhiteSpace(username);
			return !username.Equals(User.username);
		}

		/// <summary>
		/// Gets if the email is changed.
		/// </summary>
		/// <returns><c>true</c>, if the email is changed, <c>false</c> otherwise.</returns>
		/// <param name="email">Email.</param>
		bool EmailIsChanged(string email)
		{
			if (string.IsNullOrWhiteSpace(User.email)) return !string.IsNullOrWhiteSpace(email);
			return !email.Equals(User.email);
		}

		/// <summary>
		/// Gets if phone is changed.
		/// </summary>
		/// <returns><c>true</c>, if phone is changed, <c>false</c> otherwise.</returns>
		/// <param name="phone">Phone.</param>
		bool PhoneIsChanged(string phone)
		{
			if (string.IsNullOrWhiteSpace(User.phone)) return !string.IsNullOrWhiteSpace(phone);
			return !phone.Equals(User.phone);
		}

		/// <summary>
		/// Gets if passwords are changed.
		/// </summary>
		/// <returns><c>true</c>, if passwords are changed, <c>false</c> otherwise.</returns>
		/// <param name="password">Password.</param>
		/// <param name="repeatPassword">Repeat password.</param>
		public static bool PasswordsAreChanged(string password, string repeatPassword)
		{
			return !string.IsNullOrWhiteSpace(password) || !string.IsNullOrWhiteSpace(repeatPassword);
		}

		/// <summary>
		/// Gets if the password is a match.
		/// </summary>
		/// <returns><c>true</c>, if password is a match, <c>false</c> otherwise.</returns>
		/// <param name="password">Password.</param>
		/// <param name="verifyPassword">Repeat password.</param>
		bool PasswordIsMatch(string password, string verifyPassword)
		{
			if (!string.IsNullOrWhiteSpace(password) && !string.IsNullOrWhiteSpace(verifyPassword)) {
				return password.Equals(verifyPassword);
			}
			return false;
		}

		/// <summary>
		/// Gets the requests for the user.
		/// </summary>
		/// <param name="OnSuccess">Action On success.</param>
		/// <param name="IfException">Action If exception.</param>
		public void GetRequests(Action OnSuccess, Action<Exception> IfException)
		{
			Session.TaskFactory.StartNew(delegate {
				try {
					Requests = RequestApi.GetRequests(User);
					if (Requests == null) Requests = new Request[0];
					OnSuccess.Invoke();
				} catch (Exception e) {
					IfException.Invoke(e);
				}
			});
		}

		/// <summary>
		/// Gets if fields are changed.
		/// </summary>
		/// <returns><c>true</c>, if fields are changed, <c>false</c> otherwise.</returns>
		/// <param name="name">Name.</param>
		/// <param name="username">Username.</param>
		/// <param name="email">Email.</param>
		/// <param name="phone">Phone.</param>
		/// <param name="password">Password.</param>
		/// <param name="verifyPassword">Repeat password.</param>
		public bool FieldsAreChanged(string name, string username, string email, string phone, string password, string verifyPassword)
		{
			return NameIsChanged(name) || UsernameIsChanged(username) || EmailIsChanged(email) || PhoneIsChanged(phone) || PasswordsAreChanged(password, verifyPassword);
		}

		/// <summary>
		/// Handles the save button press.
		/// </summary>
		/// <returns>The api response.</returns>
		/// <param name="name">Name.</param>
		/// <param name="username">Username.</param>
		/// <param name="email">Email.</param>
		/// <param name="phone">Phone.</param>
		/// <param name="oldPassword">Old password.</param>
		/// <param name="newPassword">New password.</param>
		/// <param name="repeatNewPassword">Repeat new password.</param>
		public string OnSaveButtonPressHandler(string name, string username, string email, string phone, string oldPassword, string newPassword, string repeatNewPassword)
		{
			string result = "";
			var jsonBody = new JObject();

			// Make sure a first and last name has been given
			if (NameIsChanged(name) && name.Split(' ').Length >= 2) {
				// Get the first and last names
				string[] names = name.Split(' ');
				string firstName = names[0];
				string lastName = names[1];
				for (int i = 2; i < names.Length; i++) {
					lastName += " " + names[i];
				}
				jsonBody.Add("firstname", firstName);
				jsonBody.Add("lastname", lastName);
			}
			if (UsernameIsChanged(username)) {
				jsonBody.Add("username", username);
			}
			if (EmailIsChanged(email)) {
				jsonBody.Add("email", email);
			}
			if (PhoneIsChanged(phone)) {
				string numbers = phone.GetNumbers();
				if (numbers.Length > 0) {
					jsonBody.Add("phone", numbers);
				}
			}
			// Patch the user if something above has changed
			if (jsonBody.Count > 0) {
				result += UserApi.PatchUser(User.id, jsonBody.ToString());
			}
			if (PasswordsAreChanged(newPassword, repeatNewPassword) && PasswordIsMatch(newPassword, repeatNewPassword)
				&& !string.IsNullOrWhiteSpace(oldPassword) && Validator.Password(newPassword)) {
				jsonBody = new JObject();
				jsonBody.Add("old_password", oldPassword);
				jsonBody.Add("new_password", newPassword);
				result += UserApi.ChangePassword(User.id, jsonBody.ToString());
			}
			if (string.IsNullOrWhiteSpace(result)) {
				throw new Exception("No changes were made");
			}
			return result;
		}
	}
}
