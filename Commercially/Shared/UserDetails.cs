using System;
using System.Linq;
using Newtonsoft.Json.Linq;

namespace Commercially
{
	public class UserDetails
	{
		public User User;
		public Request[] Requests;
		public const double HeaderHeight = 50;
		public const double RowHeight = 88;
		public const double RowAlphaDouble = 0.33;
		public const byte RowAlphaByte = 0x54;
		public readonly static string HeaderTitle = RequestStatusType.Assigned.ToString();
		public readonly static Color TableHeaderColor = GlobalConstants.DefaultColors.Yellow;

		public string NameText {
			get {
				return User.firstname + " " + User.lastname;
			}
		}

		public string UsernameText {
			get {
				return User.username;
			}
		}

		public string EmailText {
			get {
				return User.email;
			}
		}

		public string PhoneText {
			get {
				return User.phone;
			}
		}

		public bool ChangePasswordButtonIsHidden {
			get {
				return !User.id.Equals(Session.User.id);
			}
		}

		bool NameChanged(string name)
		{
			if (string.IsNullOrWhiteSpace(User.firstname) || string.IsNullOrWhiteSpace(User.lastname)) return !string.IsNullOrWhiteSpace(name);
			return !name.Equals(User.firstname + " " + User.lastname);
		}

		bool UsernameChanged(string username)
		{
			if (string.IsNullOrWhiteSpace(User.username)) return !string.IsNullOrWhiteSpace(username);
			return !username.Equals(User.username);
		}

		bool EmailChanged(string email)
		{
			if (string.IsNullOrWhiteSpace(User.email)) return !string.IsNullOrWhiteSpace(email);
			return !email.Equals(User.email);
		}

		bool PhoneChanged(string phone)
		{
			if (string.IsNullOrWhiteSpace(User.phone)) return !string.IsNullOrWhiteSpace(phone);
			return !phone.Equals(User.phone);
		}

		public static bool PasswordsChanged(string password, string repeatPassword)
		{
			return !string.IsNullOrWhiteSpace(password) || !string.IsNullOrWhiteSpace(repeatPassword);
		}

		bool PasswordsMatch(string password, string repeatPassword)
		{
			if (!string.IsNullOrWhiteSpace(password) && !string.IsNullOrWhiteSpace(repeatPassword)) {
				return password.Equals(repeatPassword);
			}
			return false;
		}

		public void GetRequests(Action OnSuccess, Action<Exception> IfException)
		{
			Session.TaskFactory.StartNew(delegate {
				try {
					Requests = RequestApi.GetRequests(User);
					OnSuccess.Invoke();
				} catch (Exception e) {
					IfException.Invoke(e);
				}
			});
		}

		public bool FieldsChanged(string name, string username, string email, string phone, string password, string repeatPassword)
		{
			return NameChanged(name) || UsernameChanged(username) || EmailChanged(email) || PhoneChanged(phone) || PasswordsChanged(password, repeatPassword);
		}

		public string SaveButtonPress(string name, string username, string email, string phone, string oldPassword, string newPassword, string repeatNewPassword)
		{
			string result = "";
			var jsonBody = new JObject();
			if (NameChanged(name) && name.Split(' ').Length >= 2) {
				string[] names = name.Split(' ');
				string firstName = names[0];
				string lastName = names[1];
				for (int i = 2; i < names.Length; i++) {
					lastName += " " + names[i];
				}
				jsonBody.Add("firstname", firstName);
				jsonBody.Add("lastname", lastName);
			}
			if (UsernameChanged(username)) {
				jsonBody.Add("username", username);
			}
			if (EmailChanged(email)) {
				jsonBody.Add("email", email);
			}
			if (PhoneChanged(phone)) {
				string numbers = phone.GetNumbers();
				if (numbers.Length > 0) {
					jsonBody.Add("phone", numbers);
				}
			}
			if (jsonBody.Count > 0) {
				result += UserApi.PatchUser(User.id, jsonBody.ToString());
			}
			if (PasswordsChanged(newPassword, repeatNewPassword) && PasswordsMatch(newPassword, repeatNewPassword)
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
