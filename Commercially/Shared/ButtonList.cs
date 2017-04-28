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
		public readonly static Color InactiveTextColor = GlobalConstants.DefaultColors.White;
		public readonly static Color ActiveBackgroundColor = GlobalConstants.DefaultColors.White;
		public readonly static ButtonType[] ButtonTypes = { ButtonType.Paired, ButtonType.Discovered, ButtonType.Ignored };
		readonly static Color[] TypeBackgroundColors = { GlobalConstants.DefaultColors.Red, GlobalConstants.DefaultColors.Yellow, GlobalConstants.DefaultColors.Green };

		public string CurrentTypeTitle {
			get {
				return Array.Find(ButtonTypes, (ButtonType type) => { return type == CurrentType; }).ToString();
			}
		}

		public Color CurrentTypeColor {
			get {
				var index = Array.IndexOf(ButtonTypes, CurrentType);
				return TypeBackgroundColors[index];
			}
		}

		public static Color GetTypeColor(ButtonType type)
		{
			var index = Array.IndexOf(ButtonTypes, type);
			return TypeBackgroundColors[index];
		}

		public void GetButtons(Action OnSuccess, Action<Exception> IfException)
		{
			Session.TaskFactory.StartNew(delegate {
				try {
					Buttons = FlicButtonApi.GetButtons(CurrentType);
					OnSuccess.Invoke();
				} catch (Exception e) {
					IfException.Invoke(e);
				}
			});
		}
	}
}
