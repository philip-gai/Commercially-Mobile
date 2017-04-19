using System;
using System.Threading;

namespace Commercially
{
	public class Dashboard
	{
		public Request[][] RequestLists;
		public readonly static RequestStatusType[] RequestTypes = { RequestStatusType.Assigned, RequestStatusType.Completed, RequestStatusType.Cancelled };
		public readonly static string[] SectionTitles = { Localizable.Labels.MyTasks, RequestStatusType.Completed.ToString(), RequestStatusType.Cancelled.ToString() };
		public readonly static int[] SectionToArray = new int[SectionTitles.Length];
		public const double HeaderHeight = 50;
		public const double RowHeight = 88;
		public const double RowAlphaDouble = 0.33;
		public const byte RowAlphaByte = 0x54;
		public readonly static Color[] SectionBackgroundColors = { GlobalConstants.DefaultColors.Yellow, GlobalConstants.DefaultColors.Green, GlobalConstants.DefaultColors.Purple };

		public void GetRequests(RequestStatusType[] Types, Action OnSuccess, Action<Exception> IfException)
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
