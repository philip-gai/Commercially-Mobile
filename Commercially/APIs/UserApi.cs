using System;
using System.Collections.Generic;
using System.Net;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;

namespace Commercially
{
	public static class UserApi
	{
		readonly static string AuthUrl = HttpRequest.GetRequestUrl("/user/token");
		readonly static string CurrUserUrl = HttpRequest.GetRequestUrl("/api/currentuser");
		readonly static string UserUrl = HttpRequest.GetRequestUrl("/api/users/");
		static string AuthHeader {
			get {
				return "Bearer " + Session.OAuth.access_token;
			}
		}

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

		public static User GetCurrentUser()
		{
			var resp = HttpRequest.MakeRequest(HttpRequestMethodType.GET, CurrUserUrl, "", AuthHeader);
			return JsonConvert.DeserializeObject<User>(resp);
		}

		public static User[] GetUsers()
		{
			var resp = HttpRequest.MakeRequest(HttpRequestMethodType.GET, UserUrl, "", AuthHeader);
			return JsonConvert.DeserializeObject<List<User>>(resp).ToArray();
		}

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

		public static string PatchUser(string id, string body)
		{
			var resp = HttpRequest.MakeRequest(HttpRequestMethodType.PATCH, UserUrl + id, body, AuthHeader);
			return resp;
		}

		public static string PostUser(string body)
		{
			var resp = HttpRequest.MakeRequest(HttpRequestMethodType.POST, UserUrl, body, AuthHeader);
			return resp;
		}

		public static string DeleteUser(string id)
		{
			var resp = HttpRequest.MakeRequest(HttpRequestMethodType.DELETE, UserUrl + id, "", AuthHeader);
			return resp;
		}

		public static string ChangePassword(string id, string body)
		{
			var resp = HttpRequest.MakeRequest(HttpRequestMethodType.POST, UserUrl + id + "/changePassword", body, AuthHeader);
			return resp;
		}
	}
}
