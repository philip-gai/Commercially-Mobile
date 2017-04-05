using System.Collections.Generic;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;

namespace Commercially
{
	public static class RequestApi
	{
		const string Endpoint = "/api/requests/";
		readonly static string Url = HttpRequest.GetRequestUrl(Endpoint);

		public static Request[] GetRequests()
		{
			var resp = HttpRequest.MakeRequest(HttpRequestMethodType.GET, Url, "", "Bearer " + Session.OAuth.access_token);
			return JsonConvert.DeserializeObject<List<Request>>(resp).ToArray();
		}

		public static string PatchRequest(string id, string body) {
			var resp = HttpRequest.MakeRequest(HttpRequestMethodType.PATCH, Url + id, body, "Bearer " + Session.OAuth.access_token);
			return resp;
		}

		public static string UpdateRequest(string id, RequestStatusType newStatus) {
			string service = "";
			switch (newStatus) {
				case RequestStatusType.New:
					var jsonBody = new JObject();
					jsonBody.Add("status", RequestStatusType.New.ToString().ToLower());
					jsonBody.Add("assignedTo", "");
					jsonBody.Add("time_scheduled", "");
					jsonBody.Add("time_completed", "");
					return HttpRequest.MakeRequest(HttpRequestMethodType.PATCH, Url + id, jsonBody.ToString(), "Bearer " + Session.OAuth.access_token);
				case RequestStatusType.Assigned:
					service = "/claim";
					break;
				case RequestStatusType.Completed:
					service = "/complete";
					break;
				case RequestStatusType.Cancelled:
					service = "/cancel";
					break;
			}
			var resp = HttpRequest.MakeRequest(HttpRequestMethodType.POST, Url + id + service, "", "Bearer " + Session.OAuth.access_token);
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