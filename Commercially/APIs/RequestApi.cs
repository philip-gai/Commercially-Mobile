using System;
using System.Collections.Generic;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;

namespace Commercially
{
	public static class RequestApi
	{
		const string Endpoint = "/api/requests/";
		static string Url = HttpRequest.GetRequestUrl(Endpoint);

		static Random randGen = new Random();
		public static Request[] GetDummyRequests()
		{
			var RequestList = new List<Request>();
			int rand = randGen.Next() % 30 + 10;
			for (int i = 0; i < rand; i++) {
				RequestList.Add(Request.GetDummyRequest());
			}
			return RequestList.ToArray();
		}

		public static string GetDummyRequestsJson()
		{
			var requests = GetDummyRequests();
			return JsonConvert.SerializeObject(requests);
		}

		public static Request[] GetOfflineRequests()
		{
			var resp = GetDummyRequestsJson();
			return JsonConvert.DeserializeObject<Request[]>(resp);
		}

		public static Request[] GetRequests()
		{
			var resp = HttpRequest.MakeRequest(HttpRequestMethodType.GET, Url, "", "Bearer " + SessionData.OAuth.access_token);
			return JsonConvert.DeserializeObject<List<Request>>(resp).ToArray();
		}
	}
}