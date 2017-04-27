using System.Collections.Generic;
using Newtonsoft.Json;

namespace Commercially
{
	public static class ClientApi
	{
		readonly static string Url = HttpRequest.GetRequestUrl("/api/clients/");
		static string AuthHeader {
			get {
				return "Bearer " + Session.OAuth.access_token;
			}
		}

		public static Client[] GetClients()
		{
			var resp = HttpRequest.MakeRequest(HttpRequestMethodType.GET, Url, "", AuthHeader);
			return JsonConvert.DeserializeObject<List<Client>>(resp).ToArray();
		}
	}
}
