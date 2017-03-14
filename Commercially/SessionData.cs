using System.Collections.Generic;
namespace Commercially
{
	public static class SessionData
	{
		public static User User;
		public static OAuthResponse OAuth;
		public static Request[] Requests;
		public static bool TestMode = true;

		public static Request[][] GetRequestLists()
		{
			if (Requests == null || Requests.Length <= 0) return null;
			var ToDoList = new List<Request>();
			var InProgressList = new List<Request>();
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
				}
			}
			return new Request[][] { ToDoList.ToArray(), InProgressList.ToArray(), CompleteList.ToArray() };
		}
	}
}
