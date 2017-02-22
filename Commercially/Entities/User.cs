using System;

namespace Commercially {
	[Serializable]
	public class User {
		public User() {
			id = Guid.NewGuid().ToString();
		}
		public User(string email): this() {
			this.email = email.Trim().ToLower();
		}
		public User(string email, string password) : this(email) {
			this.password = password;
		}
		public string id { get; set; }
		public string email { get; set; }
		public string password { get; set; }
	}
}