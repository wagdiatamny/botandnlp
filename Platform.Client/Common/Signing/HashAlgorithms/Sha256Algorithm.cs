using System.Security.Cryptography;

namespace Platform.Client.Common.Signing.HashAlgorithms
{
	public class Sha256Algorithm : IHashAlgorithm
	{
		public string Compute(byte[] bytes)
		{
			var algorithm = SHA256.Create();
			return algorithm.ComputeHash(bytes).ToHexString().ToLower();
		}
	}
}