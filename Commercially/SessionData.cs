using System.Collections.Generic;
namespace Commercially
{
	public static class SessionData
	{
		public static User User;
		public static OAuthResponse OAuth;
		public static Request[] Requests;

		public static Request[][] GetRequestLists() {
			if (Requests == null || Requests.Length <= 0) return null;
			var ToDoList = new List<Request>();
			var InProgressList = new List<Request>();
			var CompleteList = new List<Request>();
			foreach (Request request in Requests) {
				switch (request.Status) {
					case Status.ToDo:
						ToDoList.Add(request);
						break;
					case Status.InProgress:
						InProgressList.Add(request);
						break;
					case Status.Complete:
						CompleteList.Add(request);
						break;
				}
			}
			return new Request[][] { ToDoList.ToArray(), InProgressList.ToArray(), CompleteList.ToArray() };
		}
	}
}
