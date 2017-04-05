using System;
using System.Collections.Generic;
using Newtonsoft.Json;

namespace Commercially
{
	public static class ClientApi
	{
		const string Endpoint = "/api/clients/";
		readonly static string Url = HttpRequest.GetRequestUrl(Endpoint);

		public static Client[] GetClients()
		{
			var resp = HttpRequest.MakeRequest(HttpRequestMethodType.GET, Url, "", "Bearer " + Session.OAuth.access_token);
			return JsonConvert.DeserializeObject<List<Client>>(resp).ToArray();
		}
	}
}
