// Created by Philip Gai

using System;
namespace Commercially
{
	/// <summary>
	/// Thrown if an error response is received from a server.
	/// </summary>
	public class ErrorResponseException : Exception
	{
		public ErrorResponseException(string message) : base(message) { }
	}
}
