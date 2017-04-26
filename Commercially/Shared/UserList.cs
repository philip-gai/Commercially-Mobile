using System;
namespace Commercially
{
	public class UserList
	{
		public User[] Users;
		public UserRoleType CurrentType = UserRoleType.Worker;
		public const double HeaderHeight = 50;
		public const double RowHeight = 88;
		public const double RowAlphaDouble = 0.33;
		public const byte RowAlphaByte = 0x54;
		public readonly static Color InactiveTextColor = GlobalConstants.DefaultColors.White;
		public readonly static Color ActiveBackgroundColor = GlobalConstants.DefaultColors.White;
		public readonly static UserRoleType[] UserRoleTypes = { UserRoleType.Worker, UserRoleType.Tenant, UserRoleType.Admin };
		readonly static Color[] TypeBackgroundColors = { GlobalConstants.DefaultColors.Red, GlobalConstants.DefaultColors.Yellow, GlobalConstants.DefaultColors.Green };

		public string CurrentTypeTitle {
			get {
				return Array.Find(UserRoleTypes, (UserRoleType type) => { return type == CurrentType; }).ToString() + "s";
			}
		}

		public Color CurrentTypeColor {
			get {
				var index = Array.IndexOf(UserRoleTypes, CurrentType);
				return TypeBackgroundColors[index];
			}
		}

		public static Color GetTypeColor(UserRoleType type)
		{
			var index = Array.IndexOf(UserRoleTypes, type);
			return TypeBackgroundColors[index];
		}

		public void GetUsers(Action OnSuccess, Action<Exception> IfException)
		{
			Session.TaskFactory.StartNew(delegate {
				try {
					Users = UserApi.GetUsers(CurrentType);
					OnSuccess.Invoke();
				} catch (Exception e) {
					IfException.Invoke(e);
				}
			});
		}
	}
}
