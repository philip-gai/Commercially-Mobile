// Created by Philip Gai

using System;
using Newtonsoft.Json.Linq;

namespace Commercially
{
	/// <summary>
	/// User create manager.
	/// </summary>
	public static class UserCreateManager
	{
		/// <summary>
		/// Gets if the passwords match.
		/// </summary>
		/// <returns><c>true</c>, if the passwords match, <c>false</c> otherwise.</returns>
		/// <param name="password">Password.</param>
		/// <param name="verifyPassword">Verify password.</param>
		static bool PasswordIsMatch(string password, string verifyPassword)
		{
			if (!string.IsNullOrWhiteSpace(password) && !string.IsNullOrWhiteSpace(verifyPassword)) {
				return password.Equals(verifyPassword);
			}
			return false;
		}

		/// <summary>
		/// Handles create button press.
		/// </summary>
		/// <returns>The api response.</returns>
		/// <param name="name">Name.</param>
		/// <param name="username">Username.</param>
		/// <param name="role">Role.</param>
		/// <param name="email">Email.</param>
		/// <param name="phone">Phone.</param>
		/// <param name="password">Password.</param>
		/// <param name="verifyPassword">Verify password.</param>
		public static string OnCreateButtonPressHandler(string name, string username, string role, string email, string phone, string password, string verifyPassword)
		{
			if (string.IsNullOrWhiteSpace(name) || string.IsNullOrWhiteSpace(username) || string.IsNullOrWhiteSpace(role) ||
				string.IsNullOrWhiteSpace(password) || string.IsNullOrWhiteSpace(verifyPassword)
				|| !PasswordIsMatch(password, verifyPassword) || !Validator.Password(password)) {
				throw new Exception("Illegal Arguments");
			}

			var jsonBody = new JObject();

			// Get first and last names
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
