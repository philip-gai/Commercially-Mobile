using System;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;

namespace Commercially {
	public static class UserApi {
		const string Endpoint = "/users";
		static string Url = HttpRequest.GetRequestUrl(Endpoint);

		public static User MakeUserRequest(HttpRequestMethodType Type, User tmpUser) {
			string query = null;
			string body = null;
			switch (Type) {
				case HttpRequestMethodType.GET:
					query = "?email=" + tmpUser.email.Trim().ToLower();
					break;
				case HttpRequestMethodType.POST:
					body = JsonConvert.SerializeObject(tmpUser);
					break;
				case HttpRequestMethodType.PUT:
					query = "/" + tmpUser.id;
					body = JsonConvert.SerializeObject(tmpUser);
					break;
				default:
					throw new Exception("Method not yet supported.");
			}
			string response;
			try {
				response = HttpRequest.MakeRequest(Type, Url + query, body);
			} catch (Exception e) {
				throw e;
			}
			User dbUser = null;
			switch (Type) {
				case HttpRequestMethodType.GET:
					dbUser = FromGetResponse(response);
					break;
				default:
					dbUser = JsonConvert.DeserializeObject<User>(response);
					break;
			}
			return dbUser;
		}

		public static bool UserExists(User user) {
			User tmpUser;
			try {
				tmpUser = MakeUserRequest(HttpRequestMethodType.GET, user);
			} catch (Exception e) {
				throw e;
			}
			return tmpUser != null;
		}

		public static User FromGetResponse(string response) {
			JArray array = JArray.Parse(response);
			if (array.Count != 1) return null;
			string obj = array.First.ToString();
			User user = JsonConvert.DeserializeObject<User>(obj);
			return user;
		}
	}
}
