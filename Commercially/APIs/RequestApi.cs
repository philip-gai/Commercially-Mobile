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
		public static Request[] GetDummyRequests() {
			var RequestList = new List<Request>();
			int rand = randGen.Next() % 30 + 10;
			for (int i = 0; i < rand; i++) {
				RequestList.Add(Request.GetDummyRequest());
			}
			Console.WriteLine(RequestList);
			return RequestList.ToArray();
		}

		public static Request[] GetRequests() {
			Console.WriteLine(Url);
			Console.WriteLine("Bearer " + SessionData.OAuth.access_token);
			string resp = HttpRequest.MakeRequest(HttpRequestMethodType.GET, Url, "", "Bearer " + SessionData.OAuth.access_token);
			Console.WriteLine(resp);
			return null;
		}
	}
}
