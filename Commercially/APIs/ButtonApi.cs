using System.Collections.Generic;
using Newtonsoft.Json;

namespace Commercially
{
	public static class ButtonApi
	{
		const string Endpoint = "/api/buttons/";
		readonly static string Url = HttpRequest.GetRequestUrl(Endpoint);

		public static FlicButton[] GetButtons()
		{
			var resp = HttpRequest.MakeRequest(HttpRequestMethodType.GET, Url, "", "Bearer " + SessionData.OAuth.access_token);
			return JsonConvert.DeserializeObject<List<FlicButton>>(resp).ToArray();
		}
	}
}
