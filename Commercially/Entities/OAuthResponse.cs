using System;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;

namespace Commercially
{
	[Serializable]
	public class OAuthResponse
	{
		public OAuthResponse() { }

		public OAuthResponse(string jsonResponse) {
			OAuthResponse tmpToken = JsonConvert.DeserializeObject<OAuthResponse>(jsonResponse);
			this.tokenType = tmpToken.tokenType;
			this.accessToken = tmpToken.accessToken;
			this.refreshToken = tmpToken.refreshToken;
		}

		public OAuthResponse(string tokenType, string accessToken, string refreshToken) {
			this.tokenType = tokenType;
			this.accessToken = accessToken;
			this.refreshToken = refreshToken;
		}

		string tokenType { get; }
		string accessToken { get; }
		string refreshToken { get; }
	}
}
