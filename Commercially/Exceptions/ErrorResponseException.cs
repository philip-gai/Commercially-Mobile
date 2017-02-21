using System;
namespace Commercially {
	public class ErrorResponseException : Exception {
		public ErrorResponseException(string message) : base(message) { }
	}
}
