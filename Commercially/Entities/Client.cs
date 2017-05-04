// Create by Philip Gai

using System;

namespace Commercially
{
	/// <summary>
	/// Represents a Client.
	/// </summary>
	[Serializable]
	public class Client
	{
		/// <summary>
		/// The unique identifier of the client.
		/// </summary>
		public string clientId { get; set; }

		/// <summary>
		/// The friendly name for the client.
		/// </summary>
		public string friendlyName { get; set; }

		/// <summary>
		/// Gets or sets a value indicating whether this <see cref="T:Commercially.Client"/> is authorized.
		/// </summary>
		public bool authorized { get; set; }
	}
}