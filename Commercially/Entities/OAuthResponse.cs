// Created by Philip Gai

using System;
using Newtonsoft.Json;

namespace Commercially
{
	/// <summary>
	/// Represents an OAuth response.
	/// </summary>
	[Serializable]
	public class OAuthResponse
	{
		/// <summary>
		/// Initializes a new instance of the <see cref="T:Commercially.OAuthResponse"/> class.
		/// </summary>
		public OAuthResponse() { }

		/// <summary>
		/// Initializes a new instance of the <see cref="T:Commercially.OAuthResponse"/> class.
		/// </summary>
		/// <param name="jsonResponse">Json response from the web server.</param>
		public OAuthResponse(string jsonResponse)
		{
			var tmpToken = JsonConvert.DeserializeObject<OAuthResponse>(jsonResponse);
			token_type = tmpToken.token_type;
			access_token = tmpToken.access_token;
			refresh_token = tmpToken.refresh_token;
		}

		/// <summary>
		/// Initializes a new instance of the <see cref="T:Commercially.OAuthResponse"/> class.
		/// </summary>
		/// <param name="tokenType">Token type.</param>
		/// <param name="accessToken">Access token.</param>
		/// <param name="refreshToken">Refresh token.</param>
		public OAuthResponse(string tokenType, string accessToken, string refreshToken)
		{
			token_type = tokenType;
			access_token = accessToken;
			refresh_token = refreshToken;
		}

		/// <summary>
		/// The token type of the oauth credentials.
		/// </summary>
		public string token_type { get; set; }

		/// <summary>
		/// The access token for the current session.
		/// </summary>
		public string access_token { get; set; }

		/// <summary>
		/// The refresh token for the curren session.
		/// </summary>
		public string refresh_token { get; set; }
	}
}
