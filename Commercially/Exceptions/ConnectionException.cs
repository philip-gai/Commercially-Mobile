using System;
namespace Commercially {
	public class ConnectionException : Exception {
		public ConnectionException(string message) : base(message) { }
	}
}
