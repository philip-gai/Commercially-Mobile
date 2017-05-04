// Created by Philip Gai

using System;
using System.Collections.Generic;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;

namespace Commercially
{
	/// <summary>
	/// Represents a Client API which uses the web server's REST services.
	/// </summary>
	public static class ClientApi
	{
		// The primary URL endpoint for client services.
		readonly static string Url = HttpRequest.GetRequestUrl("/api/clients/");

		// The URL used to authorize a client
		readonly static string AuthorizeUrl = HttpRequest.GetRequestUrl("/client/authorize/");

		/// <summary>
		/// Gets all clients from the web server.
		/// </summary>
		/// <returns>An array of all of the clients.</returns>
		public static Client[] GetClients()
		{
			var resp = HttpRequest.MakeRequest(HttpRequestMethodType.GET, Url, "", HttpRequest.AuthHeader);
			return JsonConvert.DeserializeObject<List<Client>>(resp).ToArray();
		}

		/// <summary>
		/// Gets all authorized or non-authorized clients from the web server.
		/// </summary>
		/// <returns>Authorized or non-authorized clients.</returns>
		/// <param name="isAuthorized">If set to <c>true</c> is authorized.</param>
		public static Client[] GetClients(bool isAuthorized)
		{
			var clients = GetClients();
			if (clients == null || clients.Length <= 0) return null;

			// Get an array of the clients that are authorized or not-authorized and omit the ignore client
			var list = Array.FindAll(clients, (Client client) => {
				return client.authorized == isAuthorized && !GlobalConstants.Strings.Ignore.Equals(client.clientId, StringComparison.CurrentCultureIgnoreCase);
			});

			// Sort the clients alphabetically by clientId
			Array.Sort(list, (Client x, Client y) => {
				return string.Compare(x.clientId, y.clientId);
			});
			return list;
		}

		/// <summary>
		/// Gets the client associated with the cliendId or friendlyName
		/// </summary>
		/// <returns>The client.</returns>
		/// <param name="clientInfo">The client's id or friendly name.</param>
		public static Client GetClient(string clientInfo)
		{
			return GetClients(new string[] { clientInfo })[0];
		}

		/// <summary>
		/// Gets an array of clients from an array of cliendIds and friendlyNames
		/// </summary>
		/// <returns>The clients that matched an id or friendly name.</returns>
		/// <param name="clientsInfo">An array of client ids and friendly names.</param>
		public static Client[] GetClients(string[] clientsInfo)
		{
			var clients = GetClients();
			var matchedClients = new Client[clientsInfo.Length];
			for (int i = 0; i < clientsInfo.Length; i++) {
				var clientInfo = clientsInfo[i];

				// Find the client that matches an id or friendly name
				matchedClients[i] = Array.Find(clients, (Client client) => {
					return client.clientId.Equals(clientInfo, StringComparison.CurrentCultureIgnoreCase)
								 || (!string.IsNullOrWhiteSpace(client.friendlyName) && client.friendlyName.Equals(clientInfo, StringComparison.CurrentCultureIgnoreCase));
				});
			}
			return matchedClients;
		}

		/// <summary>
		/// Patches the friendly name of the client.
		/// </summary>
		/// <returns>A status code string.</returns>
		/// <param name="clientId">Client identifier.</param>
		/// <param name="friendlyName">Friendly name of the client.</param>
		public static string PatchClientFriendlyName(string clientId, string friendlyName)
		{
			var body = new JObject();
			body.Add("friendlyName", friendlyName);
			var resp = HttpRequest.MakeRequest(HttpRequestMethodType.PATCH, Url + clientId, body.ToString(), HttpRequest.AuthHeader);
			return resp;
		}

		/// <summary>
		/// Authorizes the client.
		/// </summary>
		/// <returns>A status code string.</returns>
		/// <param name="clientId">Client identifier.</param>
		public static string AuthorizeClient(string clientId)
		{
			var body = new JObject();
			body.Add("response_type", "code");
			body.Add("redirect_uri", "/client/authSuccess");
			body.Add("client_id", clientId);
			return HttpRequest.MakeRequest(HttpRequestMethodType.POST, AuthorizeUrl, body.ToString(), HttpRequest.AuthHeader);
		}
	}
}
