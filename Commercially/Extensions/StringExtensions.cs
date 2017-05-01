using System.Linq;

namespace Commercially
{
	public static class StringExtensions
	{
		public static string GetNumbers(this string str) {
			return new string(str.Where(c => char.IsDigit(c)).ToArray());
		}
	}
}
