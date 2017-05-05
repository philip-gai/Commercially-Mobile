// Created by Philip Gai

using System;
using System.Collections.Generic;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;

namespace Commercially
{
	/// <summary>
	/// Represents an API to get Requests from the web server.
	/// </summary>
	public static class RequestApi
	{
		// The URL for getting requests from the server.
		readonly static string Url = HttpRequest.GetRequestUrl("/api/requests/");

		/// <summary>
		/// Gets all of the requests from the web server.
		/// </summary>
		/// <returns>The requests.</returns>
		public static Request[] GetRequests()
		{
			var resp = HttpRequest.MakeRequest(HttpRequestMethodType.GET, Url, "", HttpRequest.AuthHeader);
			var requests = JsonConvert.DeserializeObject<List<Request>>(resp).ToArray();
			// Sort requests from newest to oldest
			Array.Reverse(requests);
			return requests;
		}

		/// <summary>
		/// Gets all of the requests of the given type from the web server.
		/// </summary>
		/// <returns>The requests.</returns>
		/// <param name="type">Type.</param>
		public static Request[] GetRequests(RequestStatusType type)
		{
			var requests = GetRequests();
			if (requests == null) return null;
			var list = Array.FindAll(requests, (Request request) => {
				return request.Type == type;
			});
			return list;
		}

		/// <summary>
		/// Gets all the requests for a user.
		/// </summary>
		/// <returns>The requests.</returns>
		/// <param name="user">User.</param>
		public static Request[] GetRequests(User user)
		{
			var requests = GetRequests();
			if (requests == null) return null;
			var list = Array.FindAll(requests, (Request request) => {
				return request.assignedTo == user.username && request.Type == RequestStatusType.Assigned;
			});
			return list;
		}

		/// <summary>
		/// Patches the request.
		/// </summary>
		/// <returns>The status code string.</returns>
		/// <param name="id">The request identifier.</param>
		/// <param name="body">The json body containing changed fields.</param>
		public static string PatchRequest(string id, string body)
		{
			var resp = HttpRequest.MakeRequest(HttpRequestMethodType.PATCH, Url + id, body, HttpRequest.AuthHeader);
			return resp;
		}

		/// <summary>
		/// Updates the request.
		/// </summary>
		/// <returns>The status code string.</returns>
		/// <param name="id"> The identifier of the request to update.</param>
		/// <param name="newStatus">The new status to change the request to.</param>
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
					return HttpRequest.MakeRequest(HttpRequestMethodType.PATCH, Url + id, jsonBody.ToString(), HttpRequest.AuthHeader);
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
			var resp = HttpRequest.MakeRequest(HttpRequestMethodType.POST, Url + id + service, "", HttpRequest.AuthHeader);
			return resp;
		}

		/// <summary>
		/// Deletes the request.
		/// </summary>
		/// <returns>The status code string.</returns>
		/// <param name="id">The request identifier.</param>
		public static string DeleteRequest(string id)
		{
			var resp = HttpRequest.MakeRequest(HttpRequestMethodType.DELETE, Url + id, "", HttpRequest.AuthHeader);
			return resp;
		}
	}
}