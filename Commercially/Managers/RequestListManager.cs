// Created by Philip Gai

using System;

namespace Commercially
{
	/// <summary>
	/// Request list manager.
	/// </summary>
	public class RequestListManager
	{
		/// <summary>
		/// The requests.
		/// </summary>
		public Request[] Requests;

		/// <summary>
		/// The type of the current list.
		/// </summary>
		public RequestStatusType CurrentListType = RequestStatusType.New;

		/// <summary>
		/// The height of the table header.
		/// </summary>
		public const double TableHeaderHeight = 50;

		/// <summary>
		/// The height of the table row.
		/// </summary>
		public const double TableRowHeight = 88;

		/// <summary>
		/// The table row alpha double.
		/// </summary>
		public const double TableRowAlphaDouble = 0.33;

		/// <summary>
		/// The table row alpha byte.
		/// </summary>
		public const byte TableRowAlphaByte = 0x54;

		/// <summary>
		/// The color of the inactive text.
		/// </summary>
		public readonly static Color InactiveTextColor = GlobalConstants.DefaultColors.White;

		/// <summary>
		/// The color of the active background.
		/// </summary>
		public readonly static Color ActiveBackgroundColor = GlobalConstants.DefaultColors.White;

		/// <summary>
		/// The request types.
		/// </summary>
		public readonly static RequestStatusType[] RequestTypes = { RequestStatusType.New, RequestStatusType.Assigned, RequestStatusType.Completed, RequestStatusType.Cancelled };

		/// <summary>
		/// The list type background colors.
		/// </summary>
		readonly static Color[] ListTypeBackgroundColors = { GlobalConstants.DefaultColors.Red, GlobalConstants.DefaultColors.Yellow, GlobalConstants.DefaultColors.Green, GlobalConstants.DefaultColors.Purple };

		/// <summary>
		/// Gets the current list type title.
		/// </summary>
		/// <value>The current list type title.</value>
		public string CurrentListTypeTitle {
			get {
				return Array.Find(RequestTypes, (RequestStatusType type) => { return type == CurrentListType; }).ToString();
			}
		}

		/// <summary>
		/// Gets the color of the current list type.
		/// </summary>
		/// <value>The color of the current list type.</value>
		public Color CurrentListTypeColor {
			get {
				return GetListTypeColor(CurrentListType);
			}
		}

		/// <summary>
		/// Gets the color of the list type.
		/// </summary>
		/// <returns>The list type color.</returns>
		/// <param name="type">Type.</param>
		public static Color GetListTypeColor(RequestStatusType type)
		{
			var index = Array.IndexOf(RequestTypes, type);
			return ListTypeBackgroundColors[index];
		}

		/// <summary>
		/// Gets the requests.
		/// </summary>
		/// <param name="OnSuccess">Action On success.</param>
		/// <param name="IfException">Action If exception.</param>
		public void GetRequests(Action OnSuccess, Action<Exception> IfException)
		{
			Session.TaskFactory.StartNew(delegate {
				try {
					Requests = RequestApi.GetRequests(CurrentListType);
					if(Requests == null) Requests = new Request[0];
					OnSuccess.Invoke();
				} catch (Exception e) {
					IfException.Invoke(e);
				}
			});
		}
	}
}