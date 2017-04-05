using System.Collections.Generic;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;

namespace Commercially
{
	public static class ButtonApi
	{
		const string Endpoint = "/api/buttons/";
		readonly static string Url = HttpRequest.GetRequestUrl(Endpoint);

		public static FlicButton[] GetButtons()
		{
			var resp = HttpRequest.MakeRequest(HttpRequestMethodType.GET, Url, "", "Bearer " + Session.OAuth.access_token);
			return JsonConvert.DeserializeObject<List<FlicButton>>(resp).ToArray();
		}

		public static string PatchButton(string buttonId, string body)
		{
			var resp = HttpRequest.MakeRequest(HttpRequestMethodType.PATCH, Url + buttonId, body, "Bearer " + Session.OAuth.access_token);
			return resp;
		}

		public static string PairButton(string buttonId, string clientId)
		{
			var resp = HttpRequest.MakeRequest(HttpRequestMethodType.POST, Url + buttonId + "/pair", new JObject("client_id", clientId).ToString(), "Bearer " + Session.OAuth.access_token);
			return resp;
		}
	}
}