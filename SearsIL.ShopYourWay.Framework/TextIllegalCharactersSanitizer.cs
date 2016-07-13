using System.Text.RegularExpressions;

namespace SearsIL.ShopYourWay.Framework
{
	public interface ITextIllegalCharactersSanitizer
	{
		string Sanitize(string body);
	}

	public class TextIllegalCharactersSanitizer : ITextIllegalCharactersSanitizer
	{
		private readonly Regex _regEx = new Regex(@"[^\u0000-\u007F\u00A0]", RegexOptions.Compiled);

		public string Sanitize(string text)
		{
			return _regEx.Replace(text, string.Empty);
		}
	}
}