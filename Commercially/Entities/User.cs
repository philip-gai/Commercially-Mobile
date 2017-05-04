// Created by Philip Gai

using System;

namespace Commercially
{
	/// <summary>
	/// Represents a User, which is a worker, an admin or a tenant.
	/// </summary>
	[Serializable]
	public class User
	{
		/// <summary>
		/// The unique id given by MongoDB.
		/// </summary>
		public string _id { get; set; }

		/// <summary>
		/// The unique id assigned by the web server.
		/// </summary>
		public string id { get; set; }

		/// <summary>
		/// The first name.
		/// </summary>
		public string firstname { get; set; }

		/// <summary>
		/// The last name.
		/// </summary>
		public string lastname { get; set; }

		/// <summary>
		/// The email.
		/// </summary>
		public string email { get; set; }

		/// <summary>
		/// The phone number.
		/// </summary>
		public string phone { get; set; }

		/// <summary>
		/// The role, which is Worker, Admin, or Tenant.
		/// </summary>
		public string role { get; set; }

		/// <summary>
		/// The username.
		/// </summary>
		public string username { get; set; }

		/// <summary>
		/// Gets the role enum value based on the role string.
		/// </summary>
		public UserRoleType Type {
			get {
				if (role.Equals(UserRoleType.Admin.ToString(), StringComparison.CurrentCultureIgnoreCase)) {
					return UserRoleType.Admin;
				}
				if (role.Equals(UserRoleType.Worker.ToString(), StringComparison.CurrentCultureIgnoreCase)) {
					return UserRoleType.Worker;
				}
				if (role.Equals(UserRoleType.Tenant.ToString(), StringComparison.CurrentCultureIgnoreCase)) {
					return UserRoleType.Tenant;
				}
				return UserRoleType.Worker;
			}
		}
	}
}