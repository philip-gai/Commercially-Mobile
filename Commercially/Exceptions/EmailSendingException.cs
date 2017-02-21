using System;
namespace Commercially {
	public class EmailSendingException : Exception {
		public EmailSendingException(string message) : base(message) { }
	}
}
