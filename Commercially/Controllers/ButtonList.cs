using System;
namespace Commercially
{
	public static class ButtonList
	{
		public const double HeaderHeight = 50;
		public const double RowHeight = 88;
		public const double RowAlphaDouble = 0.33;
		public const byte RowAlphaByte = 0x54;
		public readonly static Color TableBackgroundColor = GlobalConstants.DefaultColors.TealBlue;

		public static void GetButtons(Action OnSuccess, Action<Exception> IfException)
		{
			Session.TaskFactory.StartNew(delegate {
				try {
					Session.Buttons = ButtonApi.GetButtons();
					OnSuccess.Invoke();
				} catch (Exception e) {
					IfException.Invoke(e);
				}
			});
		}
	}
}
