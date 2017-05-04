// Created by Philip Gai

namespace Commercially.Droid
{
	/// <summary>
	/// Android color extensions.
	/// </summary>
	public static class AndroidColorExtensions
	{
		/// <summary>
		/// Gets the color with the applied alpha value.
		/// </summary>
		/// <returns>The new color.</returns>
		/// <param name="color">Color.</param>
		/// <param name="alpha">Alpha.</param>
		public static Android.Graphics.Color ColorWithAlpha(this Android.Graphics.Color color, byte alpha)
		{
			Android.Graphics.Color newColor = color;
			newColor.A = alpha;
			return newColor;
		}
	}
}
