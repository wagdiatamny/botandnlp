using System;
using System.Collections.Generic;
using System.Text;
using Platform.Client.Common.Signing.HashAlgorithms;
using SearsIL.ShopYourWay.Framework;

namespace Platform.Client.Common.Signing
{
	public class SignatureBuilder
	{
		private readonly IHashAlgorithm _hashAlgorithm;
		private readonly List<byte> _input = new List<byte>();

		public SignatureBuilder(IHashAlgorithm hashAlgorithm = null)
		{
			if (hashAlgorithm == null)
				hashAlgorithm = new Sha256Algorithm();

			_hashAlgorithm = hashAlgorithm;
		}

		public SignatureBuilder Append(IList<byte> bytes)
		{
			_input.AddRange(bytes);
			return this;
		}

		public string Create()
		{
			return _hashAlgorithm.Compute(_input.ToArray());
		}
	}

	public static class SignatureBuilderExtensions
	{
		public static SignatureBuilder Append(this SignatureBuilder builder, long val)
		{
			var bytes = BitConverter.GetBytes(val);
			return builder.Append(bytes);
		}

		public static SignatureBuilder Append(this SignatureBuilder builder, DateTime val)
		{
			var bytes = BitConverter.GetBytes(val.ToUnixTime());
			return builder.Append(bytes);
		}

		public static SignatureBuilder Append(this SignatureBuilder builder, string val)
		{
			var bytes = Encoding.UTF8.GetBytes(val);
			return builder.Append(bytes);
		}

		public static SignatureBuilder Append(this SignatureBuilder builder, Guid value)
		{
			return builder.Append(value.ToByteArray());
		}
	}
}
