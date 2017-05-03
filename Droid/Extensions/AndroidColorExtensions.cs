using System;
namespace Commercially.Droid
{
	public static class AndroidColorExtensions
	{
		public static Android.Graphics.Color ColorWithAlpha(this Android.Graphics.Color color, byte alpha)
		{
			Android.Graphics.Color newColor = color;
			newColor.A = alpha;
			return newColor;
		}
	}
}
