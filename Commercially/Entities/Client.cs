﻿using System;

namespace Commercially
{
	[Serializable]
	public class Client
	{
		public string clientId { get; set; }
		public string friendlyName { get; set; }

		public static Client FindClient(string clientId, Client[] clients)
		{
			foreach (Client tmpClient in clients) {
				if (tmpClient.clientId.Equals(clientId)) {
					return tmpClient;
				}
			}
			return null;
		}
	}
}