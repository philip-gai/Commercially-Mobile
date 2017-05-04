// Created by Philip Gai

using System;
namespace Commercially
{
	/// <summary>
	/// User list manager.
	/// </summary>
	public class UserListManager
	{
		/// <summary>
		/// The users.
		/// </summary>
		public User[] Users;

		/// <summary>
		/// The type of the current list.
		/// </summary>
		public UserRoleType CurrentListType = UserRoleType.Worker;

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
		/// The user role types.
		/// </summary>
		public readonly static UserRoleType[] UserRoleTypes = { UserRoleType.Worker, UserRoleType.Tenant, UserRoleType.Admin };

		/// <summary>
		/// The list type background colors.
		/// </summary>
		readonly static Color[] ListTypeBackgroundColors = { GlobalConstants.DefaultColors.Red, GlobalConstants.DefaultColors.Yellow, GlobalConstants.DefaultColors.Green };

		/// <summary>
		/// Gets the current list type title.
		/// </summary>
		/// <value>The current list type title.</value>
		public string CurrentListTypeTitle {
			get {
				return Array.Find(UserRoleTypes, (UserRoleType type) => { return type == CurrentListType; }).ToString() + "s";
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
		/// Gets if can edit row.
		/// </summary>
		/// <returns><c>true</c>, if can edit row, <c>false</c> otherwise.</returns>
		/// <param name="row">Row.</param>
		public bool CanEditRow(int row)
		{
			return CanEditRow(Users[row]);
		}

		/// <summary>
		/// Gets if can edit row.
		/// </summary>
		/// <returns><c>true</c>, if can edit row, <c>false</c> otherwise.</returns>
		/// <param name="user">User.</param>
		public static bool CanEditRow(User user)
		{
			return Session.User.Type == UserRoleType.Admin && !user._id.Equals(Session.User._id);
		}

		/// <summary>
		/// Gets the color of the list type.
		/// </summary>
		/// <returns>The list type color.</returns>
		/// <param name="type">Type.</param>
		public static Color GetListTypeColor(UserRoleType type)
		{
			var index = Array.IndexOf(UserRoleTypes, type);
			return ListTypeBackgroundColors[index];
		}

		/// <summary>
		/// Gets the users based on current list type.
		/// </summary>
		/// <param name="OnSuccess">Action On success.</param>
		/// <param name="IfException">Action If exception.</param>
		public void GetUsers(Action OnSuccess, Action<Exception> IfException)
		{
			Session.TaskFactory.StartNew(delegate {
				try {
					Users = UserApi.GetUsers(CurrentListType);
					OnSuccess.Invoke();
				} catch (Exception e) {
					IfException.Invoke(e);
				}
			});
		}
	}
}
