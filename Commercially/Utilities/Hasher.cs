using System;
using System.Linq;
using System.Security.Cryptography;
using Microsoft.AspNetCore.Cryptography.KeyDerivation;

namespace Commercially {
	public static class Hasher {
		const byte version = 0x01;
		const int iterCount = 10000;
		const int numBytesRequested = 256 / 8; // 32
		const int saltSize = 128 / 8;

		public static string HashAndSalt(string password) {
			RandomNumberGenerator rng = RandomNumberGenerator.Create();
			byte[] salt = new byte[saltSize];
			rng.GetBytes(salt);
			KeyDerivationPrf prf = KeyDerivationPrf.HMACSHA256;
			byte[] subkey = KeyDerivation.Pbkdf2(password, salt, prf, iterCount, numBytesRequested);
			byte[] outputBytes = new byte[13 + salt.Length + subkey.Length];
			outputBytes[0] = version; // format marker
			WriteNetworkByteOrder(outputBytes, 1, (uint)prf);
			WriteNetworkByteOrder(outputBytes, 5, iterCount);
			WriteNetworkByteOrder(outputBytes, 9, saltSize);
			Buffer.BlockCopy(salt, 0, outputBytes, 13, salt.Length);
			Buffer.BlockCopy(subkey, 0, outputBytes, 13 + saltSize, subkey.Length);
			return Convert.ToBase64String(outputBytes);
		}
		public static bool VerifyHashedValue(string hashedPassword, string providedPassword) {
			var decodedHashedPassword = Convert.FromBase64String(hashedPassword);
			if (decodedHashedPassword[0] != version) return false;
			// Read header information
			var prf = (KeyDerivationPrf)ReadNetworkByteOrder(decodedHashedPassword, 1);
			var saltLength = (int)ReadNetworkByteOrder(decodedHashedPassword, 9);

			// Read the salt: must be >= 128 bits
			if (saltLength < saltSize) {
				return false;
			}
			var salt = new byte[saltLength];
			Buffer.BlockCopy(decodedHashedPassword, 13, salt, 0, salt.Length);

			// Read the subkey (the rest of the payload): must be >= 128 bits
			var subkeyLength = decodedHashedPassword.Length - 13 - salt.Length;
			if (subkeyLength < saltSize) {
				return false;
			}
			var expectedSubkey = new byte[subkeyLength];
			Buffer.BlockCopy(decodedHashedPassword, 13 + salt.Length, expectedSubkey, 0, expectedSubkey.Length);

			// Hash the incoming password and verify it
			var actualSubkey = KeyDerivation.Pbkdf2(providedPassword, salt, prf, iterCount, subkeyLength);
			return actualSubkey.SequenceEqual(expectedSubkey);
		}

		static void WriteNetworkByteOrder(byte[] buffer, int offset, uint value) {
			buffer[offset + 0] = (byte)(value >> 24);
			buffer[offset + 1] = (byte)(value >> 16);
			buffer[offset + 2] = (byte)(value >> 8);
			buffer[offset + 3] = (byte)(value >> 0);
		}

		static uint ReadNetworkByteOrder(byte[] buffer, int offset) {
			return ((uint)(buffer[offset + 0]) << 24)
				| ((uint)(buffer[offset + 1]) << 16)
				| ((uint)(buffer[offset + 2]) << 8)
				| ((uint)(buffer[offset + 3]));
		}
	}
}
