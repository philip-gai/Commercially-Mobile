using System;
namespace Commercially
{
	public class RequestList
	{
		public Request[] NewRequestList;

		public const double HeaderHeight = 50;
		public const double RowHeight = 88;
		public const double RowAlphaDouble = 0.33;
		public const byte RowAlphaByte = 0x54;
		public readonly static Color TableBackgroundColor = GlobalConstants.DefaultColors.Red;

		public void GetRequests(Action OnSuccess, Action<Exception> IfException)
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
