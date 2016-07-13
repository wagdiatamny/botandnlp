using System;

namespace Platform.Client.Configuration
{
	public interface IPlatformSettings
	{
		Uri SywWebSiteUrl { get; }
		Uri ApiUrl { get; }
	}
}