using Platform.Client.Common.Signing;
using Platform.Client.Configuration;

namespace Platform.Client
{
	public interface IPlatformHashProvider
	{
		string GetHash();
	}

	public class PlatformHashProvider : IPlatformHashProvider
	{
		private readonly IApplicationSettings _applicationSettings;
		private readonly IPlatformTokenProvider _platfromTokenProvider;

		public PlatformHashProvider(IApplicationSettings applicationSettings,
			IPlatformTokenProvider platformTokenProvider)
		{
			_applicationSettings = applicationSettings;
			_platfromTokenProvider = platformTokenProvider;
		}

		public string GetHash()
		{
			return new SignatureBuilder()
				.Append(_platfromTokenProvider.Get())
				.Append(_applicationSettings.AppSecret)
				.Create();
		}
	}
}
