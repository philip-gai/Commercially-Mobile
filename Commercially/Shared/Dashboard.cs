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
		public readonly static Color InactiveColor = GlobalConstants.DefaultColors.Black;

		public readonly static RequestStatusType[] SectionTypes = { RequestStatusType.New, RequestStatusType.Assigned, RequestStatusType.Completed, RequestStatusType.Cancelled };
		readonly static Color[] SectionBackgroundColors = { GlobalConstants.DefaultColors.Red, GlobalConstants.DefaultColors.Yellow, GlobalConstants.DefaultColors.Green, GlobalConstants.DefaultColors.Purple };

		public string SectionTitle {
			get {
				return Array.Find(SectionTypes, (RequestStatusType type) => { return type == CurrentType; }).ToString();
			}
		}

		public Color SectionColor {
			get {
				var index = Array.IndexOf(SectionTypes, CurrentType);
				return SectionBackgroundColors[index];
			}
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
