// Create by Philip Gai

using System;
using System.Collections.Generic;
using System.Net;
using Newtonsoft.Json;

namespace Commercially
{
	/// <summary>
	/// Represents the User API for users from the web server.
	/// </summary>
	public static class UserApi
	{
		// Endpoint for authorizing / logging in a user
		readonly static string AuthUrl = HttpRequest.GetRequestUrl("/user/token");

		// Endpoint for getting the current logged in user
		readonly static string CurrUserUrl = HttpRequest.GetRequestUrl("/api/currentuser");

		// Endpoint for general user services
		readonly static string UserUrl = HttpRequest.GetRequestUrl("/api/users/");

		/// <summary>
		/// Logs the user in.
		/// </summary>
		/// <returns>The status code response.</returns>
		/// <param name="email">The user's email.</param>
		/// <param name="password">The user's password.</param>
		public static string LoginUser(string email, string password)
		{
			const string grantType = "grant_type=password";
			const string clientId = "client_id=MobileApp";
			const string clientSecret = "client_secret=null";
			const string contentType = @"application/x-www-form-urlencoded";
			return HttpRequest.MakeRequest(
				HttpRequestMethodType.POST,
				AuthUrl,
				grantType + "&username=" + WebUtility.UrlEncode(email) + "&password=" +
				WebUtility.UrlEncode(password) + "&" + clientId + "&" + clientSecret, "",
				contentType);
		}

		/// <summary>
		/// Gets the current user.
		/// </summary>
		/// <returns>The current user.</returns>
		public static User GetCurrentUser()
		{
			var resp = HttpRequest.MakeRequest(HttpRequestMethodType.GET, CurrUserUrl, "", HttpRequest.AuthHeader);
			return JsonConvert.DeserializeObject<User>(resp);
		}

		/// <summary>
		/// Gets all users from the web server.
		/// </summary>
		/// <returns>The users.</returns>
		public static User[] GetUsers()
		{
			var resp = HttpRequest.MakeRequest(HttpRequestMethodType.GET, UserUrl, "", HttpRequest.AuthHeader);
			return JsonConvert.DeserializeObject<List<User>>(resp).ToArray();
		}

		/// <summary>
		/// Gets the users based on user role.
		/// </summary>
		/// <returns>The users that have a certain role, sorted alphabetically by username.</returns>
		/// <param name="type">The type of user.</param>
		public static User[] GetUsers(UserRoleType type)
		{
			var users = GetUsers();
			if (users == null || users.Length <= 0) return null;
			var list = Array.FindAll(users, (User user) => {
				return user.Type == type;
			});
			Array.Sort(list, (User x, User y) => {
				return string.Compare(x.username, y.username);
			});
			return list;
		}

		/// <summary>
		/// Patches the user.
		/// </summary>
		/// <returns>The status code string.</returns>
		/// <param name="id">The user's id.</param>
		/// <param name="body">The body of fields to change.</param>
		public static string PatchUser(string id, string body)
		{
			var resp = HttpRequest.MakeRequest(HttpRequestMethodType.PATCH, UserUrl + id, body, HttpRequest.AuthHeader);
			return resp;
		}

		/// <summary>
		/// Posts the user.
		/// </summary>
		/// <returns>The status code.</returns>
		/// <param name="body">The body of the user to create.</param>
		public static string PostUser(string body)
		{
			var resp = HttpRequest.MakeRequest(HttpRequestMethodType.POST, UserUrl, body, HttpRequest.AuthHeader);
			return resp;
		}

		/// <summary>
		/// Deletes the user.
		/// </summary>
		/// <returns>The user.</returns>
		/// <param name="id">The user's id.</param>
		public static string DeleteUser(string id)
		{
			var resp = HttpRequest.MakeRequest(HttpRequestMethodType.DELETE, UserUrl + id, "", HttpRequest.AuthHeader);
			return resp;
		}

		/// <summary>
		/// Changes the user's password.
		/// </summary>
		/// <returns>The status code.</returns>
		/// <param name="id">The user's id.</param>
		/// <param name="body">The body containing the new password.</param>
		public static string ChangePassword(string id, string body)
		{
			var resp = HttpRequest.MakeRequest(HttpRequestMethodType.POST, UserUrl + id + "/changePassword", body, HttpRequest.AuthHeader);
			return resp;
		}
	}
}