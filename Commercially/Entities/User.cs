using System;

namespace Commercially {
	[Serializable]
	public class User {
		public User() {
		}
		public string _id { get; set; }
		public string firstname { get; set; }
		public string lastname { get; set; }
		public string email { get; set; }
		public string phone { get; set; }
		// "Worker", "Admin", "Tenant" (possibly)
		public string role { get; set; }
		public string username { get; set; }

		public UserRoleType GetUserRoleType() {
			if (role.Equals(UserRoleType.Admin.ToString())) {
				return UserRoleType.Admin;	
			}
			if (role.Equals(UserRoleType.Worker.ToString())) {
				return UserRoleType.Worker;	
			}
			if (role.Equals(UserRoleType.Tenant.ToString())) {
				return UserRoleType.Tenant;	
			}
			return UserRoleType.Worker;
		}
	}
}