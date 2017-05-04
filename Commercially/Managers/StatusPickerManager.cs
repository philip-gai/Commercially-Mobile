// Created by Philip Gai

namespace Commercially
{
	/// <summary>
	/// Status picker manager.
	/// </summary>
	public static class StatusPickerManager
	{
		/// <summary>
		/// The statuses.
		/// </summary>
		public readonly static string[] Statuses = { RequestStatusType.New.ToString(), RequestStatusType.Assigned.ToString(), RequestStatusType.Completed.ToString(), RequestStatusType.Cancelled.ToString() };
	}
}
