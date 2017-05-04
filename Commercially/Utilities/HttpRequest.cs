// Created by Philip Gai

using System.IO;
using System.Net;
using System.Text;

namespace Commercially
{
	/// <summary>
	/// Makes an Http request.
	/// </summary>
	public static class HttpRequest
	{
		/// <summary>
		/// Gets the request URL.
		/// </summary>
		/// <returns>The request URL.</returns>
		/// <param name="Endpoint">Endpoint.</param>
		public static string GetRequestUrl(string Endpoint)
		{
			return "https://" + GlobalConstants.ServerUrl + ":" + GlobalConstants.ServerPort + Endpoint;
		}

		/// <summary>
		/// Gets the auth header.
		/// </summary>
		/// <value>The auth header.</value>
		public static string AuthHeader {
			get {
				return "Bearer " + Session.OAuth.access_token;
			}
		}

		/// <summary>
		/// Makes the request.
		/// </summary>
		/// <returns>The request.</returns>
		/// <param name="Type">The Http Request Method Type.</param>
		/// <param name="url">URL.</param>
		/// <param name="body">Body.</param>
		/// <param name="authHeader">Auth header.</param>
		/// <param name="contentType">Content type.</param>
		public static string MakeRequest(HttpRequestMethodType Type, string url, string body = "", string authHeader = "", string contentType = "")
		{
			var request = (HttpWebRequest)WebRequest.Create(url);
			request.Headers.Add("Authorization", authHeader);

			// Sets the request method and other necessary fields
			switch (Type) {
				case HttpRequestMethodType.GET:
					request.Method = WebRequestMethods.Http.Get;
					break;
				case HttpRequestMethodType.POST:
					request.Method = WebRequestMethods.Http.Post;
					request.ContentType = @"application/json";
					break;
				case HttpRequestMethodType.PUT:
					request.Method = WebRequestMethods.Http.Put;
					break;
				case HttpRequestMethodType.PATCH:
					request.Method = "PATCH";
					request.Expect = "False";
					request.ContentType = @"application/json";
					request.Headers.Add("X-Http-Method-Override", "PATCH");
					break;
				case HttpRequestMethodType.DELETE:
					request.Method = "DELETE";
					break;
			}
			if (!string.IsNullOrWhiteSpace(contentType)) {
				request.ContentType = contentType;
			}
			try {
				// Place the body in the request
				if (Type == HttpRequestMethodType.POST || Type == HttpRequestMethodType.PUT || Type == HttpRequestMethodType.PATCH) {
					var encoding = new UTF8Encoding();
					var byteArray = encoding.GetBytes(body);
					request.ContentLength = byteArray.Length;
					using (Stream dataStream = request.GetRequestStream()) {    // THIS line causing hangup when can't connect to server
						dataStream.Write(byteArray, 0, byteArray.Length);
					}
				}
				// Get the request response
				var response = request.GetResponse() as HttpWebResponse;
				using (Stream responseStream = response.GetResponseStream()) {
					var reader = new StreamReader(responseStream, Encoding.UTF8);
					var json = reader.ReadToEnd();
					return json;
				}
				// Throw exceptions based on response
			} catch (WebException ex) {
				WebResponse errorResponse = ex.Response;
				if (errorResponse == null) {
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
