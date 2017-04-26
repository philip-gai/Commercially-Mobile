using System;
using System.Threading;

namespace Commercially
{
	public class Dashboard
	{
		public Request[] Requests;
		public RequestStatusType CurrentType = RequestStatusType.New;
		public const double HeaderHeight = 50;
		public const double RowHeight = 88;
		public const double RowAlphaDouble = 0.33;
		public const byte RowAlphaByte = 0x54;
		public readonly static Color InactiveTextColor = GlobalConstants.DefaultColors.White;
		public readonly static Color ActiveBackgroundColor = GlobalConstants.DefaultColors.White;

		public readonly static RequestStatusType[] RequestTypes = { RequestStatusType.New, RequestStatusType.Assigned, RequestStatusType.Completed, RequestStatusType.Cancelled };
		readonly static Color[] TypeBackgroundColors = { GlobalConstants.DefaultColors.Red, GlobalConstants.DefaultColors.Yellow, GlobalConstants.DefaultColors.Green, GlobalConstants.DefaultColors.Purple };

		public string CurrentTypeTitle {
			get {
				return Array.Find(RequestTypes, (RequestStatusType type) => { return type == CurrentType; }).ToString();
			}
		}

		public Color CurrentTypeColor {
			get {
				var index = Array.IndexOf(RequestTypes, CurrentType);
				return TypeBackgroundColors[index];
			}
		}

		public static Color GetTypeColor(RequestStatusType type) {
			var index = Array.IndexOf(RequestTypes, type);
			return TypeBackgroundColors[index];
		}

		public void GetRequests(Action OnSuccess, Action<Exception> IfException)
		{
			Session.TaskFactory.StartNew(delegate {
				try {
					Requests = Request.GetRequests(CurrentType);
					OnSuccess.Invoke();
				} catch (Exception e) {
					IfException.Invoke(e);
				}
			});
		}
	}
}
