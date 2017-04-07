using System;
namespace Commercially
{
	public static class RequestList
	{
		public static double HeaderHeight = 50;
		public static double RowHeight = 88;
		public static Request[] NewRequestList;
		public static double RowAlphaDouble = 0.33;
		public static byte RowAlphaByte = 0x54;
		public static Color TableBackgroundColor = GlobalConstants.DefaultColors.Red;

		public static void GetRequests(Action OnSuccess, Action<Exception> IfException)
		{
			Session.TaskFactory.StartNew(delegate {
				try {
					Session.Requests = RequestApi.GetRequests();
					NewRequestList = Request.GetRequestLists(Session.Requests, new RequestStatusType[] { RequestStatusType.New })[0];
					OnSuccess.Invoke();
				} catch (Exception e) {
					IfException.Invoke(e);
				}
			});
		}
	}
}
