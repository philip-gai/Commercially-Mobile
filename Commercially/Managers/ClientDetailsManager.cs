// Created by Philip Gai

namespace Commercially
{
	/// <summary>
	/// Manages the client details business logic.
	/// </summary>
	public class ClientDetailsManager
	{
		/// <summary>
		/// The client.
		/// </summary>
		public Client Client;

		/// <summary>
		/// The duration of the animation.
		/// </summary>
		public const double AnimationDuration = 0.25;

		/// <summary>
		/// Gets the identifier text.
		/// </summary>
		/// <value>The identifier text.</value>
		public string IdText {
			get {
				return Client.clientId;
			}
		}

		/// <summary>
		/// Gets the friendly name text.
		/// </summary>
		/// <value>The friendly name text.</value>
		public string FriendlyNameText {
			get {
				return Client.friendlyName;
			}
		}

		/// <summary>
		/// Gets the friendly name field placeholder.
		/// </summary>
		/// <value>The friendly name field placeholder.</value>
		public string FriendlyNameFieldPlaceholder {
			get {
				return "Raspberry 1";
			}
		}

		/// <summary>
		/// Gets a value indicating whether this <see cref="T:Commercially.ClientDetailsManager"/> authorize button is hidden.
		/// </summary>
		/// <value><c>true</c> if authorize button is hidden; otherwise, <c>false</c>.</value>
		public bool AuthorizeButtonIsHidden {
			get {
				return Client.authorized;
			}
		}

		/// <summary>
		/// Handles save button press.
		/// </summary>
		/// <param name="friendlyName">The new friendly name to save.</param>
		public void OnSaveButtonPressHandler(string friendlyName)
		{
			if (FriendlyNameIsChanged(friendlyName) && !string.IsNullOrWhiteSpace(friendlyName)) {
				ClientApi.PatchClientFriendlyName(Client.clientId, friendlyName);
			}
		}

		/// <summary>
		/// Handles authorizing client when authorize button is pressed.
		/// </summary>
		public void OnAuthorizeButtonPressHandler()
		{
			ClientApi.AuthorizeClient(Client.clientId);
		}

		/// <summary>
		/// Gets if the friendly name is changed.
		/// </summary>
		/// <returns><c>true</c>, if name is friendly name is changed, <c>false</c> otherwise.</returns>
		/// <param name="friendlyNameText">Friendly name text.</param>
		public bool FriendlyNameIsChanged(string friendlyNameText)
		{
			if (string.IsNullOrWhiteSpace(Client.friendlyName)) return true;
			return !Client.friendlyName.Equals(friendlyNameText);
		}
	}
}