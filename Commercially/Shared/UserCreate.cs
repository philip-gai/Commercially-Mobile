using System;
using Newtonsoft.Json.Linq;

namespace Commercially
{
	public static class UserCreate
	{
		static bool PasswordsMatch(string password, string repeatPassword)
		{
			if (!string.IsNullOrWhiteSpace(password) && !string.IsNullOrWhiteSpace(repeatPassword)) {
				return password.Equals(repeatPassword);
			}
			return false;
		}

		public static string CreateButtonPress(string name, string username, string role, string email, string phone, string password, string verifyPassword)
		{

			if (string.IsNullOrWhiteSpace(name) || string.IsNullOrWhiteSpace(username) || string.IsNullOrWhiteSpace(role) ||
				string.IsNullOrWhiteSpace(password) || string.IsNullOrWhiteSpace(verifyPassword)
				|| !PasswordsMatch(password, verifyPassword) || !Validator.Password(password)) {
				throw new Exception("Illegal Arguments");
			}

			var jsonBody = new JObject();
			string[] names = name.Split(' ');
			string firstName = names[0];
			string lastName = names[1];
			for (int i = 2; i < names.Length; i++) {
				lastName += " " + names[i];
			}

			jsonBody.Add("firstname", firstName);
			jsonBody.Add("lastname", lastName);
			jsonBody.Add("username", username);
			jsonBody.Add("role", role);
			jsonBody.Add("password", password);

			// Optional values
			if (!string.IsNullOrWhiteSpace(email) && Validator.Email(email)) {
				jsonBody.Add("email", email);
			}
			if (!string.IsNullOrWhiteSpace(phone.GetNumbers())) {
				jsonBody.Add("phone", phone.GetNumbers());
			}
			return UserApi.PostUser(jsonBody.ToString());
		}
	}
}
