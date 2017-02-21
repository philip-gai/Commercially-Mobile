using System;
namespace Commercially {
	public class EmailAlreadySentException : Exception {
		public EmailAlreadySentException(string message) : base(message) { }
	}
}
