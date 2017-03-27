using System;
namespace Commercially
{
	public static class StringExtensions
	{
		public static RequestStatusType? GetStatus(this string status)
		{
			if (string.IsNullOrWhiteSpace(status)) {
				return null;
			}

			const string NewStr = "new";
			const string assigned = "assigned";
			const string completed = "completed";
			const string cancelled = "cancelled";


			switch (status.ToLower()) {
				case NewStr:
					return RequestStatusType.New;
				case assigned:
					return RequestStatusType.Assigned;
				case completed:
					return RequestStatusType.Completed;
				case cancelled:
					return RequestStatusType.Cancelled;
				default:
					return null;
			}
		}
	}
}
