namespace Commercially.Droid
{
	public static class ColorExtensions
	{
		public static Android.Graphics.Color GetAndroidColor(this Color color)
		{
			return new Android.Graphics.Color(color.R, color.G, color.B);
		}

		public static Android.Graphics.Color ColorWithAlpha(this Color color, byte alpha)
		{
			return new Android.Graphics.Color(color.R, color.G, color.B).ColorWithAlpha(alpha);
		}
	}
}
