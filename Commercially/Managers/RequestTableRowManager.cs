// Created by Philip Gai

namespace Commercially
{
	/// <summary>
	/// Request table row manager.
	/// </summary>
	public class RequestTableRowManager
	{
		/// <summary>
		/// The request.
		/// </summary>
		public readonly Request Request;

		/// <summary>
		/// Initializes a new instance of the <see cref="T:Commercially.RequestTableRowManager"/> class.
		/// </summary>
		/// <param name="request">Request.</param>
		public RequestTableRowManager(Request request)
		{
			Request = request;
		}

		/// <summary>
		/// Gets the location text.
		/// </summary>
		/// <value>The location text.</value>
		public string LocationText {
			get {
				return Request.room;
			}
		}

		/// <summary>
		/// Gets the received time text.
		/// </summary>
		/// <value>The received time text.</value>
		public string ReceivedTimeText {
			get {
				return Request.GetTime(Request.TimeType.Received)?.ToShortTimeString();
			}
		}

		/// <summary>
		/// Gets the status text.
		/// </summary>
		/// <value>The status text.</value>
		public string StatusText {
			get {
				return Request.Type.ToString();
			}
		}

		/// <summary>
		/// Gets the description text.
		/// </summary>
		/// <value>The description text.</value>
		public string DescriptionText {
			get {
				return Request.description;
			}
		}

		/// <summary>
		/// Gets a value indicating whether this <see cref="T:Commercially.RequestTableRowManager"/> status label is hidden.
		/// </summary>
		/// <value><c>true</c> if status label is hidden; otherwise, <c>false</c>.</value>
		public bool StatusLabelIsHidden {
			get {
				return true;
			}
		}

		/// <summary>
		/// Gets a value indicating whether this <see cref="T:Commercially.RequestTableRowManager"/> urgent indicator is hidden.
		/// </summary>
		/// <value><c>true</c> if urgent indicator is hidden; otherwise, <c>false</c>.</value>
		public bool UrgentIndicatorIsHidden {
			get {
				return !Request.urgent;
			}
		}
	}
}
