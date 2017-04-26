using System;
namespace Commercially
{
	public class ButtonList
	{
		public FlicButton[] Buttons;
		public ButtonType CurrentType = ButtonType.Paired;
		public const double HeaderHeight = 50;
		public const double RowHeight = 88;
		public const double RowAlphaDouble = 0.33;
		public const byte RowAlphaByte = 0x54;
		public readonly static Color TableBackgroundColor = GlobalConstants.DefaultColors.TealBlue;
		public readonly static Color ActiveColor = GlobalConstants.DefaultColors.TealBlue;
		public readonly static Color InactiveColor = GlobalConstants.DefaultColors.Black;
		public readonly static ButtonType[] ButtonTypes = { ButtonType.Paired, ButtonType.Discovered, ButtonType.Ignored };

		public string SectionTitle {
			get {
				return Array.Find(ButtonTypes, (ButtonType type) => { return type == CurrentType; }).ToString();
			}
		}

		public void GetButtons(Action OnSuccess, Action<Exception> IfException)
		{
			Session.TaskFactory.StartNew(delegate {
				try {
					Buttons = FlicButton.GetButtons(CurrentType);
					OnSuccess.Invoke();
				} catch (Exception e) {
					IfException.Invoke(e);
				}
			});
		}
	}
}
