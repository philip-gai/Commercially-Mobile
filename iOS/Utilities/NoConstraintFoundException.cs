using System;
namespace Commercially.iOS {
	public class NoConstraintMatchingException : Exception {
		public NoConstraintMatchingException(string message) : base(message) { }
	}
}
