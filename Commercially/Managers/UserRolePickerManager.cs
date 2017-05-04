// Created by Philip Gai

namespace Commercially
{
	/// <summary>
	/// User role picker manager.
	/// </summary>
	public static class UserRolePickerManager
	{
		/// <summary>
		/// The roles.
		/// </summary>
		public readonly static string[] Roles = { UserRoleType.Worker.ToString(), UserRoleType.Tenant.ToString(), UserRoleType.Admin.ToString() };
	}
}