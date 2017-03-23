using System;
namespace Commercially
{
	public static class DateTimeExtensions
	{
		public static DateTime? ConvertToDateTime(this string milliseconds)
		{
			if (milliseconds == null) return null;
			var dateTimeOffset = DateTimeOffset.FromUnixTimeMilliseconds(long.Parse(milliseconds));
			return dateTimeOffset.UtcDateTime;
		}

		public static long ConvertToMilliseconds(this DateTime date)
		{
			var tempDate = DateTime.SpecifyKind(date, DateTimeKind.Utc);
			DateTimeOffset dateTimeOffset = tempDate;
			return dateTimeOffset.ToUnixTimeMilliseconds();
		}
	}
}
