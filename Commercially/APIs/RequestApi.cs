using System;
using System.Collections.Generic;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;

namespace Commercially
{
	public static class RequestApi
	{
		readonly static string Url = HttpRequest.GetRequestUrl("/api/requests/");
		static string AuthHeader {
			get {
				return "Bearer " + Session.OAuth.access_token;
			}
		}

		public static Request[] GetRequests()
		{
			var resp = HttpRequest.MakeRequest(HttpRequestMethodType.GET, Url, "", AuthHeader);
			return JsonConvert.DeserializeObject<List<Request>>(resp).ToArray();
		}

		public static Request[] GetRequests(RequestStatusType type)
		{
			var requests = GetRequests();
			if (requests == null || requests.Length <= 0) return null;
			var list = Array.FindAll(requests, (Request request) => {
				return request.Type == type;
			});
			Array.Reverse(list);
			return list;
		}

		public static Request[] GetRequests(User user)
		{
			var requests = GetRequests();
			if (requests == null || requests.Length <= 0) return null;
			var list = Array.FindAll(requests, (Request request) => {
				return request.assignedTo == user.username && request.Type == RequestStatusType.Assigned;
			});
			Array.Reverse(list);
			return list;
		}

		public static string PatchRequest(string id, string body)
		{
			var resp = HttpRequest.MakeRequest(HttpRequestMethodType.PATCH, Url + id, body, AuthHeader);
			return resp;
		}

		public static string UpdateRequest(string id, RequestStatusType newStatus)
		{
			string service = "";
			switch (newStatus) {
				case RequestStatusType.New:
					var jsonBody = new JObject();
					jsonBody.Add("status", RequestStatusType.New.ToString().ToLower());
					jsonBody.Add("assignedTo", "");
					jsonBody.Add("time_scheduled", "");
					jsonBody.Add("time_completed", "");
					return HttpRequest.MakeRequest(HttpRequestMethodType.PATCH, Url + id, jsonBody.ToString(), AuthHeader);
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
			var resp = HttpRequest.MakeRequest(HttpRequestMethodType.POST, Url + id + service, "", AuthHeader);
			return resp;
		}

		public static string DeleteRequest(string id)
		{
			var resp = HttpRequest.MakeRequest(HttpRequestMethodType.DELETE, Url + id, "", AuthHeader);
			return resp;
		}
	}
}