using System;
namespace Commercially
{
	public class RequestTableRow
	{
		public readonly Request Request;

		public RequestTableRow(Request request)
		{
			Request = request;
		}

		public string LocationText {
			get {
				return Request.room;
			}
		}

		public string TimeText {
			get {
				return Request.GetTime(Request.TimeType.Received)?.ToShortTimeString();
			}
		}

		public string StatusText {
			get {
				return Request.Type.ToString();
			}
		}

		public string DescriptionText {
			get {
				return Request.description;
			}
		}

		public bool StatusLabelIsHidden {
			get {
				return true;
			}
		}

		public bool UrgentIndicatorIsHidden {
			get {
				return !Request.urgent;
			}
		}
	}
}
