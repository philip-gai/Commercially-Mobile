namespace Commercially
{
	public class Color
	{
		byte _R;
		public byte R {
			get {
				return _R;
			}
		}

		byte _G;
		public byte G {
			get {
				return _G;
			}
		}

		byte _B;
		public byte B {
			get {
				return _B;
			}
		}

		public Color(byte R, byte G, byte B) {
			_R = R;
			_G = G;
			_B = B;
		}
	}
}
