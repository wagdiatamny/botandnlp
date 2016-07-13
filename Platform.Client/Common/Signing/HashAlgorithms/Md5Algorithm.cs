using System.Security.Cryptography;

namespace Platform.Client.Common.Signing.HashAlgorithms
{
	public class Md5Algorithm : IHashAlgorithm
	{
		public string Compute(byte[] bytes)
		{
			var algorithm = MD5.Create();
			return algorithm.ComputeHash(bytes).ToHexString().ToLower();
		}
	}
}