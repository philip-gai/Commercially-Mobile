using System;
using System.Net;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;

namespace Commercially
{
	public static class UserApi
	{
		const string Endpoint = "/user/token";
		static string Url = HttpRequest.GetRequestUrl(Endpoint);

		public static string LoginUser(string email, string password)
		{
			return HttpRequest.MakeRequest(HttpRequestMethodType.POST, Url, "grant_type=password&username=" + WebUtility.UrlEncode(email) + "&password=" + WebUtility.UrlEncode(password) + "&client_id=MobileApp&client_secret=null");
		}
	}
}
