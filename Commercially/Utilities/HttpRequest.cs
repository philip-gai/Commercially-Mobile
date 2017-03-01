using System.Net;
using System.IO;
using System.Text;

namespace Commercially
{
	public static class HttpRequest
	{
		public static string GetRequestUrl(string Endpoint)
		{
			return "https://" + GlobalConstants.ServerUrl + ":" + GlobalConstants.ServerPort + Endpoint;
		}

		public static string MakeRequest(HttpRequestMethodType Type, string url, string body = "", string authHeader = "")
		{
			var request = (HttpWebRequest)WebRequest.Create(url);
			request.Headers.Add("Authorization", authHeader);
			switch (Type) {
				case HttpRequestMethodType.GET:
					request.Method = WebRequestMethods.Http.Get;
					break;
				case HttpRequestMethodType.POST:
					request.Method = WebRequestMethods.Http.Post;
					request.ContentType = @"application/x-www-form-urlencoded";
					break;
				case HttpRequestMethodType.PUT:
					request.Method = WebRequestMethods.Http.Put;
					break;
				case HttpRequestMethodType.PATCH:
					request.Method = WebRequestMethods.Http.Post;
					request.Headers.Add("X-Http-Method-Override", "PATCH");
					break;
				case HttpRequestMethodType.DELETE:
					request.Method = "DELETE";
					break;
			}
			try {
				if (Type == HttpRequestMethodType.POST || Type == HttpRequestMethodType.PUT || Type == HttpRequestMethodType.PATCH) {
					var encoding = new UTF8Encoding();
					var byteArray = encoding.GetBytes(body);
					request.ContentLength = byteArray.Length;
					//request.ContentType = @"application/json";
					using (Stream dataStream = request.GetRequestStream()) {
						dataStream.Write(byteArray, 0, byteArray.Length);
					}
				}
				var response = request.GetResponse();
				using (Stream responseStream = response.GetResponseStream()) {
					var reader = new StreamReader(responseStream, Encoding.UTF8);
					string json = reader.ReadToEnd();
					return json;
				}
			} catch (WebException ex) {
				WebResponse errorResponse = ex.Response;
				if (errorResponse == null)
				{
					if (ex != null) { 
						throw new ConnectionException(ex.Message);
					}
					throw new ConnectionException(Localizable.ExceptionMessages.CannotConnectServer);
				}
				using (Stream responseStream = errorResponse.GetResponseStream()) {
					var reader = new StreamReader(responseStream, Encoding.GetEncoding("utf-8"));
					var errorText = reader.ReadToEnd();
					throw new ErrorResponseException(errorText);
				}
			}
		}
	}
}
