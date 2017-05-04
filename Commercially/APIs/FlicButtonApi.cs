// Created by Philip Gai

using System;
using System.Collections.Generic;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;

namespace Commercially
{
	/// <summary>
	/// Represents an API for FlicButton services.
	/// </summary>
	public static class FlicButtonApi
	{
		// The URL for api buttons endpoints.
		readonly static string Url = HttpRequest.GetRequestUrl("/api/buttons/");

		/// <summary>
		/// Gets all of the flicbuttons from the web server.
		/// </summary>
		/// <returns>The buttons.</returns>
		public static FlicButton[] GetButtons()
		{
			var resp = HttpRequest.MakeRequest(HttpRequestMethodType.GET, Url, "", HttpRequest.AuthHeader);
			var buttonList = JsonConvert.DeserializeObject<List<FlicButton>>(resp);
			return buttonList.ToArray();
		}

		/// <summary>
		/// Gets all of the flicbuttons of a certain type.
		/// </summary>
		/// <returns>The buttons that match the given type.</returns>
		/// <param name="type">The type of FlicButton.</param>
		public static FlicButton[] GetButtons(ButtonType type)
		{
			var buttons = GetButtons();
			if (buttons == null || buttons.Length <= 0) return null;
			var list = Array.FindAll(buttons, (FlicButton button) => {
				return button.Type == type;
			});
			return list;
		}

		/// <summary>
		/// Makes patch requests to a flicbutton.
		/// </summary>
		/// <returns>The status code string.</returns>
		/// <param name="buttonId">Button identifier.</param>
		/// <param name="body">The json body containing attributes to change.</param>
		public static string PatchButton(string buttonId, string body)
		{
			var resp = HttpRequest.MakeRequest(HttpRequestMethodType.PATCH, Url + buttonId, body, HttpRequest.AuthHeader);
			return resp;
		}

		/// <summary>
		/// Pairs the button to a client.
		/// </summary>
		/// <returns>The status code string.</returns>
		/// <param name="buttonId">Button identifier.</param>
		/// <param name="clientId">Client identifier.</param>
		public static string PairButton(string buttonId, string clientId)
		{
			var jsonBody = new JObject();
			jsonBody.Add("client_id", clientId);
			var resp = HttpRequest.MakeRequest(
				HttpRequestMethodType.POST,
				Url + buttonId + "/pair",
				jsonBody.ToString(),
				HttpRequest.AuthHeader);
			return resp;
		}

		/// <summary>
		/// Deletes the button.
		/// </summary>
		/// <returns>The status code string.</returns>
		/// <param name="buttonId">Button identifier.</param>
		public static string DeleteButton(string buttonId)
		{
			var resp = HttpRequest.MakeRequest(HttpRequestMethodType.DELETE, Url + buttonId, "", HttpRequest.AuthHeader);
			return resp;
		}
	}
}