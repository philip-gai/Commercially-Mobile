using System;
namespace Commercially
{
	public class ClientList
	{
		public Client[] Clients;
		public bool AuthorizedType = true;
		public const double HeaderHeight = 50;
		public const double RowHeight = 88;
		public const double RowAlphaDouble = 0.33;
		public const byte RowAlphaByte = 0x54;
		public readonly static Color InactiveTextColor = GlobalConstants.DefaultColors.White;
		public readonly static Color ActiveBackgroundColor = GlobalConstants.DefaultColors.White;
		public readonly static string[] ButtonLabels = { "Authorized", "Discovered" };
		readonly static Color[] TypeBackgroundColors = { GlobalConstants.DefaultColors.Red, GlobalConstants.DefaultColors.Yellow };

		public string CurrentTypeTitle {
			get {
				return AuthorizedType ? "Authorized" : "Discovered";
			}
		}

		public Color CurrentTypeColor {
			get {
				return GetTypeColor(AuthorizedType);
			}
		}

		public static Color GetTypeColor(bool authorized)
		{
			return TypeBackgroundColors[authorized ? 0 : 1];
		}

		public void GetClients(Action OnSuccess, Action<Exception> IfException)
		{
			Session.TaskFactory.StartNew(delegate {
				try {
					Clients = ClientApi.GetClients(AuthorizedType);
					OnSuccess.Invoke();
				} catch (Exception e) {
					IfException.Invoke(e);
				}
			});
		}
	}
}
