using System;
namespace Commercially
{
	public static class Dashboard
	{
		public static Request[][] RequestLists;
		public static string[] SectionTitles = { Localizable.Labels.MyTasks, RequestStatusType.Completed.ToString(), RequestStatusType.Cancelled.ToString() };
		public static int[] SectionToArray = new int[SectionTitles.Length];
		public static double HeaderHeight = 50;
		public static double RowHeight = 88;
		public static double RowAlpha = 0.33;
		public static Color[] SectionBackgroundColors = { GlobalConstants.DefaultColors.Yellow, GlobalConstants.DefaultColors.Green, GlobalConstants.DefaultColors.Purple };

		public static void GetRequests(Action OnSuccess, Action<Exception> IfException)
		{
			Session.TaskFactory.StartNew(delegate {
				try {
					if (Session.TestMode) {
						Session.Requests = RequestApi.GetOfflineRequests();
						RequestLists = Request.GetRequestLists(Session.Requests, new RequestStatusType[] { RequestStatusType.Assigned, RequestStatusType.Completed, RequestStatusType.Cancelled });
						OnSuccess.Invoke();
						return;
					}
					Session.Requests = RequestApi.GetRequests();
					RequestLists = Request.GetRequestLists(Session.Requests, new RequestStatusType[] { RequestStatusType.Assigned, RequestStatusType.Completed, RequestStatusType.Cancelled });
					OnSuccess.Invoke();
				} catch (Exception e) {
					IfException.Invoke(e);
				}
			});
		}
	}
}
