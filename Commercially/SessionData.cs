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

		public static Request[][] GetRequestLists(Status[] Types = null)
		{
			if (Requests == null || Requests.Length <= 0) return null;
			var NewList = new List<Request>();
			var AssignedList = new List<Request>();
			var CancelledList = new List<Request>();
			var CompletedList = new List<Request>();
			foreach (Request request in Requests) {
				switch (request.GetStatus()) {
					case Status.New:
						NewList.Add(request);
						break;
					case Status.Assigned:
						AssignedList.Add(request);
						break;
					case Status.Completed:
						CompletedList.Add(request);
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
							RequestLists.Add(NewList);
							break;
						case Status.Assigned:
							RequestLists.Add(AssignedList);
							break;
						case Status.Completed:
							RequestLists.Add(CompletedList);
							break;
						case Status.Cancelled:
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
