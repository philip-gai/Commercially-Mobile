// Created by Philip Gai

using System;

namespace Commercially.iOS
{
	/// <summary>
	/// No constraint matching exception.
	/// </summary>
	public class NoConstraintMatchingException : Exception
	{
		public NoConstraintMatchingException(string message) : base(message) { }
	}
}
