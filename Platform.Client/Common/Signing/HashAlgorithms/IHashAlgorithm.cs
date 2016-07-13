namespace Platform.Client.Common.Signing.HashAlgorithms
{
	public interface IHashAlgorithm
	{
		string Compute(byte[] bytes);
	}
}