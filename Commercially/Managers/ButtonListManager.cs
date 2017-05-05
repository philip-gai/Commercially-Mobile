// Created by Philip Gai

using System;
namespace Commercially
{
	/// <summary>
	/// Manges button list business logic.
	/// </summary>
	public class ButtonListManager
	{
		/// <summary>
		/// The buttons.
		/// </summary>
		public FlicButton[] Buttons;

		/// <summary>
		/// The type of the button list displayed.
		/// </summary>
		public ButtonType CurrentListType = ButtonType.Paired;

		/// <summary>
		/// The height of the table header.
		/// </summary>
		public const double TableHeaderHeight = 50;

		/// <summary>
		/// The height of the table row.
		/// </summary>
		public const double TableRowHeight = 88;

		/// <summary>
		/// The row alpha double.
		/// </summary>
		public const double TableRowAlphaDouble = 0.33;

		/// <summary>
		/// The row alpha byte.
		/// </summary>
		public const byte TableRowAlphaByte = 0x54;

		/// <summary>
		/// The color of the inactive text.
		/// </summary>
		public readonly static Color InactiveTextColor = GlobalConstants.DefaultColors.White;

		/// <summary>
		/// The color of the active background.
		/// </summary>
		public readonly static Color ActiveBackgroundColor = GlobalConstants.DefaultColors.White;

		/// <summary>
		/// The button list types.
		/// </summary>
		public readonly static ButtonType[] ButtonListTypes = { ButtonType.Paired, ButtonType.Discovered, ButtonType.Ignored };

		/// <summary>
		/// The type background colors.
		/// </summary>
		readonly static Color[] TypeBackgroundColors = { GlobalConstants.DefaultColors.Red, GlobalConstants.DefaultColors.Yellow, GlobalConstants.DefaultColors.Green };

		/// <summary>
		/// Gets the current list type title.
		/// </summary>
		/// <value>The current list type title.</value>
		public string CurrentListTypeTitle {
			get {
				return Array.Find(ButtonListTypes, (ButtonType type) => { return type == CurrentListType; }).ToString();
			}
		}

		/// <summary>
		/// Gets the color of the current list type.
		/// </summary>
		/// <value>The color of the current list type.</value>
		public Color CurrentListTypeColor {
			get {
				return GetListTypeColor(CurrentListType);
			}
		}

		/// <summary>
		/// Gets the color of the list type.
		/// </summary>
		/// <returns>The list type color.</returns>
		/// <param name="type">Type.</param>
		public static Color GetListTypeColor(ButtonType type)
		{
			var index = Array.IndexOf(ButtonListTypes, type);
			return TypeBackgroundColors[index];
		}

		/// <summary>
		/// Gets the buttons for the current list type.
		/// </summary>
		/// <param name="OnSuccess">Action taken after successfully getting buttons.</param>
		/// <param name="IfException">Actions taken if exception thrown when getting buttons.</param>
		public void GetButtons(Action OnSuccess, Action<Exception> IfException)
		{
			Session.TaskFactory.StartNew(delegate {
				try {
					Buttons = FlicButtonApi.GetButtons(CurrentListType);
					if (Buttons == null) Buttons = new FlicButton[0];
					OnSuccess.Invoke();
				} catch (Exception e) {
					IfException.Invoke(e);
				}
			});
		}
	}
}
