using System;

namespace Commercially {
	[Serializable]
	public class User {
		public User() {
		}
		public User(string email): this() {
			this.email = email;
		}
		public User(string email, string password) : this(email) {
			this.password = password;
		}
		public string email { get; set; }
		public string password { get; set; }
	}
}