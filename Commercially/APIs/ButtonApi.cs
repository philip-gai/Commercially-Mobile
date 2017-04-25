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
			var buttonList = JsonConvert.DeserializeObject<List<FlicButton>>(resp);
			buttonList.Reverse();
			return buttonList.ToArray();
		}

		public static string PatchButton(string buttonId, string body)
		{
			var resp = HttpRequest.MakeRequest(HttpRequestMethodType.PATCH, Url + buttonId, body, "Bearer " + Session.OAuth.access_token);
			return resp;
		}

		public static string PairButton(string buttonId, string clientId)
		{
			var jsonBody = new JObject();
			jsonBody.Add("client_id", clientId);
			var resp = HttpRequest.MakeRequest(HttpRequestMethodType.POST, Url + buttonId + "/pair", jsonBody.ToString(), "Bearer " + Session.OAuth.access_token);
			return resp;
		}

		public static string DeleteButton(string buttonId)
		{
			var resp = HttpRequest.MakeRequest(HttpRequestMethodType.DELETE, Url + buttonId, "", "Bearer " + Session.OAuth.access_token);
			return resp;
		}
	}
}