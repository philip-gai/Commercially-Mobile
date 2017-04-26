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
		readonly static string UserUrl = HttpRequest.GetRequestUrl("/api/users");
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
			var list = new List<User>();
			foreach (var user in users) {
				if (user.Type == type) {
					list.Add(user);
				}
			}
			list.Sort((User x, User y) => {
				if (x.lastname != null && y.lastname != null) {
					return string.Compare(x.lastname, y.lastname, StringComparison.Ordinal);
				}
				return string.Compare(x.email, y.email, StringComparison.Ordinal);
			});
			return list.ToArray();
		}
	}
}
