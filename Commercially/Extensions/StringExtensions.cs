// Created by Philip Gai

using System.Linq;

namespace Commercially
{
	/// <summary>
	/// String extensions.
	/// </summary>
	public static class StringExtensions
	{
		/// <summary>
		/// Gets the number chars from a string.
		/// </summary>
		/// <returns>The numbers.</returns>
		/// <param name="str">String of anything.</param>
		public static string GetNumbers(this string str)
		{
			return new string(str.Where(c => char.IsDigit(c)).ToArray());
		}
	}
}
