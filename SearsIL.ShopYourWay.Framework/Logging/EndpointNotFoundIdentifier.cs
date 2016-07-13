using System;
using System.Text.RegularExpressions;

namespace SearsIL.ShopYourWay.Framework.Logging
{
	public class EndpointNotFoundLogger : ICustomLogger
	{
		private static readonly string _actionNotFound = "A public action method '.*' was not found on controller";
		private static readonly string _controllerForPathNotFound = "The controller for path '.*' was not found or does not implement IController";
		private readonly Regex _regex = new Regex($"{_actionNotFound}|{_controllerForPathNotFound}", RegexOptions.Compiled);

		public bool Log(Exception exception, ILogger logger)
		{
			if (!_regex.IsMatch(exception.Message))
				return false;

			logger.Warn(exception);
			return true;
		}
	}
}