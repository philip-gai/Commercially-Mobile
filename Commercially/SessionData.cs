using System.Linq;
using System.Collections.Generic;
using System.Threading.Tasks;
namespace Commercially
{
	public static class SessionData
	{
		public static User User;
		public static OAuthResponse OAuth;
		public static Request[] Requests;
		public static bool TestMode;
		public static TaskFactory TaskFactory = new TaskFactory();

		public static Request[][] GetRequestLists(RequestStatusType[] Types = null)
		{
			if (Requests == null || Requests.Length <= 0) return null;
			var NewList = new List<Request>();
			var AssignedList = new List<Request>();
			var CancelledList = new List<Request>();
			var CompletedList = new List<Request>();
			foreach (Request request in Requests) {
				switch (request.GetStatus()) {
					case RequestStatusType.New:
						NewList.Add(request);
						break;
					case RequestStatusType.Assigned:
						AssignedList.Add(request);
						break;
					case RequestStatusType.Completed:
						CompletedList.Add(request);
						break;
					case RequestStatusType.Cancelled:
						CancelledList.Add(request);
						break;
				}
			}
			if (Types != null) {
				var RequestLists = new List<List<Request>>();
				foreach (RequestStatusType type in Types) {
					switch (type) {
						case RequestStatusType.New:
							RequestLists.Add(NewList);
							break;
						case RequestStatusType.Assigned:
							RequestLists.Add(AssignedList);
							break;
						case RequestStatusType.Completed:
							RequestLists.Add(CompletedList);
							break;
						case RequestStatusType.Cancelled:
							RequestLists.Add(CancelledList);
							break;
					}
				}
				return RequestLists.Select(list => list.ToArray()).ToArray();
			}

			return new Request[][] { NewList.ToArray(), AssignedList.ToArray(), CompletedList.ToArray(), CancelledList.ToArray() };
		}
	}
}
