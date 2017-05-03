using System;
using System.Collections.Generic;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;

namespace Commercially
{
	public static class FlicButtonApi
	{
		readonly static string Url = HttpRequest.GetRequestUrl("/api/buttons/");
		static string AuthHeader {
			get {
				return "Bearer " + Session.OAuth.access_token;
			}
		}

		public static FlicButton[] GetButtons()
		{
			var resp = HttpRequest.MakeRequest(HttpRequestMethodType.GET, Url, "", AuthHeader);
			var buttonList = JsonConvert.DeserializeObject<List<FlicButton>>(resp);
			buttonList.Reverse();
			return buttonList.ToArray();
		}

		public static FlicButton[] GetButtons(ButtonType type)
		{
			var buttons = GetButtons();
			if (buttons == null || buttons.Length <= 0) return null;
			var list = Array.FindAll(buttons, (FlicButton button) => {
				return button.Type == type;
			});
			Array.Reverse(list);
			return list;
		}

		public static string PatchButton(string buttonId, string body)
		{
			var resp = HttpRequest.MakeRequest(HttpRequestMethodType.PATCH, Url + buttonId, body, AuthHeader);
			return resp;
		}

		public static string PairButton(string buttonId, string clientId)
		{
			var jsonBody = new JObject();
			jsonBody.Add("client_id", clientId);
			var resp = HttpRequest.MakeRequest(
				HttpRequestMethodType.POST,
				Url + buttonId + "/pair",
				jsonBody.ToString(),
				AuthHeader);
			return resp;
		}

		public static string DeleteButton(string buttonId)
		{
			var resp = HttpRequest.MakeRequest(HttpRequestMethodType.DELETE, Url + buttonId, "", AuthHeader);
			return resp;
		}
	}
}