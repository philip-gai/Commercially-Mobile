// Created by Philip Gai

namespace Commercially.Droid
{
	/// <summary>
	/// Color extensions.
	/// </summary>
	public static class ColorExtensions
	{
		/// <summary>
		/// Gets the android color from the Color color.
		/// </summary>
		/// <returns>The android color.</returns>
		/// <param name="color">Color.</param>
		public static Android.Graphics.Color GetAndroidColor(this Color color)
		{
			return new Android.Graphics.Color(color.R, color.G, color.B);
		}

		/// <summary>
		/// Gets the color with the alpha value applied.
		/// </summary>
		/// <returns>The color with alpha.</returns>
		/// <param name="color">Color.</param>
		/// <param name="alpha">Alpha.</param>
		public static Android.Graphics.Color ColorWithAlpha(this Color color, byte alpha)
		{
			return new Android.Graphics.Color(color.R, color.G, color.B).ColorWithAlpha(alpha);
		}
	}
}
