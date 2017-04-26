using System;
using System.Collections.Generic;

namespace Commercially
{
	[Serializable]
	public class User
	{
		public string _id { get; set; }
		public string firstname { get; set; }
		public string lastname { get; set; }
		public string email { get; set; }
		public string phone { get; set; }
		// "Worker", "Admin", "Tenant" (possibly)
		public string role { get; set; }
		public string username { get; set; }

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

		internal UserRoleType GetUserRoleType {
			get {
				throw new NotImplementedException();
			}
		}
	}
}