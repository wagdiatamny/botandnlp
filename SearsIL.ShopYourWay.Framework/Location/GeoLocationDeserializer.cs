using System;
using System.Text.RegularExpressions;
using SearsIL.ShopYourWay.Framework.Logging;

namespace SearsIL.ShopYourWay.Framework.Location
{
	public interface IGeoLocationDeserializer
	{
		GeoLocation Deserialize(string rawGeoLocation);
	}

	public class GeoLocationDeserializer : IGeoLocationDeserializer
	{
		private readonly ILogger _logger;

		public GeoLocationDeserializer(ILogger logger)
		{
			_logger = logger;
		}

		private readonly Regex _geoLocationExtractorRegex = new Regex(@"^(?<continent>[A-Za-z]+) (?<country>[A-Za-z]+) ", RegexOptions.Compiled);

		public GeoLocation Deserialize(string rawGeoLocation)
		{
			if (string.IsNullOrEmpty(rawGeoLocation)) throw new ArgumentNullException(nameof(rawGeoLocation));

			var match = _geoLocationExtractorRegex.Match(rawGeoLocation);

			var continent = match.Groups["continent"].Value;
			var country = match.Groups["country"].Value;

			if (!string.IsNullOrEmpty(continent) && !string.IsNullOrEmpty(country))
				return new GeoLocation { ContinentPrefix = continent, CountryPrefix = country, Raw = rawGeoLocation };

			_logger.Error($"failed to parse geolocation - {rawGeoLocation}");
			return null;
		}
	}
}