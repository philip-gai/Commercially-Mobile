using System.Linq;
using System.Collections.Generic;
namespace Commercially
{
	public static class SessionData
	{
		public static User User;
		public static OAuthResponse OAuth;
		public static Request[] Requests;
		public static bool TestMode = true;

		public static Request[][] GetRequestLists(Status[] Types = null)
		{
			if (Requests == null || Requests.Length <= 0) return null;
			var ToDoList = new List<Request>();
			var InProgressList = new List<Request>();
			var CancelledList = new List<Request>();
			var CompleteList = new List<Request>();
			foreach (Request request in Requests) {
				switch (request.GetStatus()) {
					case Status.New:
						ToDoList.Add(request);
						break;
					case Status.Assigned:
						InProgressList.Add(request);
						break;
					case Status.Completed:
						CompleteList.Add(request);
						break;
					case Status.Cancelled:
						CancelledList.Add(request);
						break;
				}
			}
			if (Types != null) {
				var RequestLists = new List<List<Request>>();
				foreach (Status type in Types) {
					switch (type) {
						case Status.New:
							RequestLists.Add(ToDoList);
							break;
						case Status.Assigned:
							RequestLists.Add(InProgressList);
							break;
						case Status.Completed:
							RequestLists.Add(CompleteList);
							break;
						case Status.Cancelled:
							RequestLists.Add(CancelledList);
							break;
					}
				}
				return RequestLists.Select(a => a.ToArray()).ToArray();
			}

			return new Request[][] { ToDoList.ToArray(), InProgressList.ToArray(), CompleteList.ToArray(), CancelledList.ToArray() };
		}
	}
}
