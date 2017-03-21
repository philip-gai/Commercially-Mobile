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

		public static Request[] GetRequests()
		{
			var resp = HttpRequest.MakeRequest(HttpRequestMethodType.GET, Url, "", "Bearer " + SessionData.OAuth.access_token);
			return JsonConvert.DeserializeObject<List<Request>>(resp).ToArray();
		}

		public static string PatchRequest(string id, string body) {
			var resp = HttpRequest.MakeRequest(HttpRequestMethodType.PATCH, Url + id, body, "Bearer " + SessionData.OAuth.access_token);
			return resp;
		}

		public static string ClaimRequest(string id) {
			var resp = HttpRequest.MakeRequest(HttpRequestMethodType.POST, Url + id + "/claim", "", "Bearer " + SessionData.OAuth.access_token);
			return resp;
		}

		public static Request[] GetDummyRequests()
		{
			var RequestList = new List<Request>();
			for (int i = 0; i < 30; i++) {
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
	}
}