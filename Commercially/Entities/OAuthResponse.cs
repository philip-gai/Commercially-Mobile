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
			var tmpToken = JsonConvert.DeserializeObject<OAuthResponse>(jsonResponse);
			token_type = tmpToken.token_type;
			access_token = tmpToken.access_token;
			refresh_token = tmpToken.refresh_token;
		}

		public OAuthResponse(string tokenType, string accessToken, string refreshToken) {
			token_type = tokenType;
			access_token = accessToken;
			refresh_token = refreshToken;
		}

		public string token_type { get; set; }
		public string access_token { get; set; }
		public string refresh_token { get; set; }
	}
}
