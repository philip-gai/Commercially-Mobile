using System;
namespace Commercially
{
	public static class DateTimeExtensions
	{
		static readonly DateTime UnixEpoch = new DateTime(1970, 1, 1, 0, 0, 0, 0);

		public static DateTime ConvertToDateTime(this string jsonDate)
		{
			var milliseconds = Convert.ToDouble(jsonDate);
			var dateTime = UnixEpoch.AddMilliseconds(milliseconds).ToLocalTime();
			return dateTime;
		}

		public static long ConvertToJsonTime(this DateTime date)
		{
			var timeSpan = date.ToLocalTime().Subtract(UnixEpoch);
			return (long)Math.Truncate(timeSpan.TotalSeconds);
		}
	}
}
