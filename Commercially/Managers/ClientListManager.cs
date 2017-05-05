// Created by Philip Gai

using System;
namespace Commercially
{
	/// <summary>
	/// Manages the business logic for the client list.
	/// </summary>
	public class ClientListManager
	{
		/// <summary>
		/// The clients.
		/// </summary>
		public Client[] Clients;

		/// <summary>
		/// The type of the authorized.
		/// </summary>
		public bool AuthorizedListType = true;

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
		/// The button labels.
		/// </summary>
		public readonly static string[] ButtonLabels = { "Authorized", "Discovered" };

		/// <summary>
		/// The list type background colors.
		/// </summary>
		readonly static Color[] ListTypeBackgroundColors = { GlobalConstants.DefaultColors.Red, GlobalConstants.DefaultColors.Yellow };

		/// <summary>
		/// Gets the current list type title.
		/// </summary>
		/// <value>The current list type title.</value>
		public string CurrentListTypeTitle {
			get {
				return AuthorizedListType ? "Authorized" : "Discovered";
			}
		}

		/// <summary>
		/// Gets the color of the current list type.
		/// </summary>
		/// <value>The color of the current list type.</value>
		public Color CurrentListTypeColor {
			get {
				return GetListTypeColor(AuthorizedListType);
			}
		}

		/// <summary>
		/// Gets the color of the list type.
		/// </summary>
		/// <returns>The list type color.</returns>
		/// <param name="authorized">If set to <c>true</c> authorized.</param>
		public static Color GetListTypeColor(bool authorized)
		{
			return ListTypeBackgroundColors[authorized ? 0 : 1];
		}

		/// <summary>
		/// Gets the clients of authorizedlisttype.
		/// </summary>
		/// <param name="OnSuccess">Action On success.</param>
		/// <param name="IfException">Action If exception.</param>
		public void GetClients(Action OnSuccess, Action<Exception> IfException)
		{
			Session.TaskFactory.StartNew(delegate {
				try {
					Clients = ClientApi.GetClients(AuthorizedListType);
					if (Clients == null) Clients = new Client[0];
					OnSuccess.Invoke();
				} catch (Exception e) {
					IfException.Invoke(e);
				}
			});
		}
	}
}
