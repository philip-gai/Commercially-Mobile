// Created by Philip Gai

namespace Commercially
{
	/// <summary>
	/// Represents an RGB color.
	/// </summary>
	public class Color
	{
		/// <summary>
		/// Initializes a new instance of the <see cref="T:Commercially.Color"/> class.
		/// </summary>
		/// <param name="R">R.</param>
		/// <param name="G">G.</param>
		/// <param name="B">B.</param>
		public Color(byte R, byte G, byte B)
		{
			_R = R;
			_G = G;
			_B = B;
		}

		/// <summary>
		/// The red value.
		/// </summary>
		readonly byte _R;
		public byte R {
			get {
				return _R;
			}
		}

		/// <summary>
		/// The green value.
		/// </summary>
		readonly byte _G;
		public byte G {
			get {
				return _G;
			}
		}

		/// <summary>
		/// The blue value.
		/// </summary>
		readonly byte _B;
		public byte B {
			get {
				return _B;
			}
		}
	}
}