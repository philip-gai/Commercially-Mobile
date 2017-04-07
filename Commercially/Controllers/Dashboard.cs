using System;
namespace Commercially
{
	public static class Dashboard
	{
		public static Request[][] RequestLists;
		public static RequestStatusType[] RequestTypes = { RequestStatusType.Assigned, RequestStatusType.Completed, RequestStatusType.Cancelled };
		public static string[] SectionTitles = { Localizable.Labels.MyTasks, RequestStatusType.Completed.ToString(), RequestStatusType.Cancelled.ToString() };
		public static int[] SectionToArray = new int[SectionTitles.Length];
		public static double HeaderHeight = 50;
		public static double RowHeight = 88;
		public static double RowAlphaDouble = 0.33;
		public static byte RowAlphaByte = 0x54;
		public static Color[] SectionBackgroundColors = { GlobalConstants.DefaultColors.Yellow, GlobalConstants.DefaultColors.Green, GlobalConstants.DefaultColors.Purple };

		public static void GetRequests(RequestStatusType[] Types, Action OnSuccess, Action<Exception> IfException)
		{
			Session.TaskFactory.StartNew(delegate {
				try {
					Session.Requests = RequestApi.GetRequests();
					RequestLists = Request.GetRequestLists(Session.Requests, Types);
					OnSuccess.Invoke();
				} catch (Exception e) {
					IfException.Invoke(e);
				}
			});
		}
	}
}
