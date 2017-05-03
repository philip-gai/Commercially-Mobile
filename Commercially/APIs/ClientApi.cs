using System;
using System.Collections.Generic;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;

namespace Commercially
{
	public static class ClientApi
	{
		readonly static string Url = HttpRequest.GetRequestUrl("/api/clients/");
		readonly static string AuthorizeUrl = HttpRequest.GetRequestUrl("/client/authorize/");
		static string AuthHeader {
			get {
				return "Bearer " + Session.OAuth.access_token;
			}
		}

		public static Client[] GetClients()
		{
			var resp = HttpRequest.MakeRequest(HttpRequestMethodType.GET, Url, "", AuthHeader);
			return JsonConvert.DeserializeObject<List<Client>>(resp).ToArray();
		}

		public static Client[] GetClients(bool isAuthorized)
		{
			var clients = GetClients();
			if (clients == null || clients.Length <= 0) return null;
			var list = Array.FindAll(clients, (Client client) => {
				return client.authorized == isAuthorized && !GlobalConstants.Strings.Ignore.Equals(client.clientId, StringComparison.CurrentCultureIgnoreCase);
			});
			Array.Sort(list, (Client x, Client y) => {
				return string.Compare(x.clientId, y.clientId);
			});
			return list;
		}

		public static Client GetClient(string clientInfo)
		{
			return GetClients(new string[] { clientInfo })[0];
		}

		public static Client[] GetClients(string[] clientsInfo)
		{
			var clients = GetClients();
			var matchedClients = new Client[clientsInfo.Length];
			for (int i = 0; i < clientsInfo.Length; i++) {
				var clientInfo = clientsInfo[i];
				matchedClients[i] = Array.Find(clients, (Client client) => {
					return client.clientId.Equals(clientInfo, StringComparison.CurrentCultureIgnoreCase)
								 || (!string.IsNullOrWhiteSpace(client.friendlyName) && client.friendlyName.Equals(clientInfo, StringComparison.CurrentCultureIgnoreCase));
				});
			}
			return matchedClients;
		}

		public static string PatchClientFriendlyName(string clientId, string friendlyName)
		{
			var body = new JObject();
			body.Add("friendlyName", friendlyName);
			var resp = HttpRequest.MakeRequest(HttpRequestMethodType.PATCH, Url + clientId, body.ToString(), AuthHeader);
			return resp;
		}

		public static string AuthorizeClient(string clientId)
		{
			var body = new JObject();
			body.Add("response_type", "code");
			body.Add("redirect_uri", "/client/authSuccess");
			body.Add("client_id", clientId);
			return HttpRequest.MakeRequest(HttpRequestMethodType.POST, AuthorizeUrl, body.ToString(), AuthHeader);
		}
	}
}
