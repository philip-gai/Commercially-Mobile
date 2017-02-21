using System;
namespace Commercially {
	public class ObjectExistsException : Exception {
		public ObjectExistsException(string message) : base(message) { }
	}
}
