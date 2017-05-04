// Created by Philip Gai

using System;
namespace Commercially
{
	/// <summary>
	/// Date time extensions.
	/// </summary>
	public static class DateTimeExtensions
	{
		/// <summary>
		/// Converts to date time.
		/// </summary>
		/// <returns>The date time.</returns>
		/// <param name="milliseconds">The date time represented in milliseconds.</param>
		public static DateTime? ConvertToDateTime(this string milliseconds)
		{
			if (string.IsNullOrWhiteSpace(milliseconds)) return null;
			var dateTimeOffset = DateTimeOffset.FromUnixTimeMilliseconds(long.Parse(milliseconds));
			return dateTimeOffset.UtcDateTime;
		}

		/// <summary>
		/// Converts to milliseconds.
		/// </summary>
		/// <returns>The milliseconds.</returns>
		/// <param name="date">The date represented as DateTime object.</param>
		public static long ConvertToMilliseconds(this DateTime date)
		{
			var tempDate = DateTime.SpecifyKind(date, DateTimeKind.Utc);
			DateTimeOffset dateTimeOffset = tempDate;
			return dateTimeOffset.ToUnixTimeMilliseconds();
		}
	}
}
