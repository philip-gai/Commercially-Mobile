// Created by Philip Gai

using System;
namespace Commercially
{
	/// <summary>
	/// Represents an exception thrown from bad connection to a server.
	/// </summary>
	public class ConnectionException : Exception
	{
		public ConnectionException(string message) : base(message) { }
	}
}
