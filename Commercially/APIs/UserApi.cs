using System;
using System.Net;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;

namespace Commercially
{
	public static class UserApi
	{
		const string Endpoint = "/user/token";
		readonly static string Url = HttpRequest.GetRequestUrl(Endpoint);
		const string CurrUserEndpoint = "/api/currentuser";

		public static string LoginUser(string email, string password)
		{
			return HttpRequest.MakeRequest(HttpRequestMethodType.POST, Url, "grant_type=password&username=" + WebUtility.UrlEncode(email) + "&password=" + WebUtility.UrlEncode(password) + "&client_id=MobileApp&client_secret=null", "", @"application/x-www-form-urlencoded");
		}

		public static User GetCurrentUser() {
			var resp = HttpRequest.MakeRequest(HttpRequestMethodType.GET, HttpRequest.GetRequestUrl(CurrUserEndpoint), "", "Bearer " + Session.OAuth.access_token);
			return JsonConvert.DeserializeObject<User>(resp);
		}
	}
}
