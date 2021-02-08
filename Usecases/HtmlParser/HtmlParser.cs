using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;

namespace Sympli.Search.Usecases.HtmlParser
{
    public class HtmlParser : IHtmlParser
    {
        public string Parse(List<string> pages, string targetURL, string pattern)
        {
            var positions = new List<int>();

            var html = Concatenate(pages);

            ParseHtml(html, targetURL, pattern, positions);

            return Convert(positions);
        }

        private static void ParseHtml(string html, string targetURL, string pattern, List<int> positions)
        {
            var matches = Regex.Matches(html, pattern, RegexOptions.Multiline | RegexOptions.IgnoreCase);

            for (var index = 0; index < matches.Count; index++)
            {
                var value = matches[index].Groups[0].Value;
                if (value.Contains(targetURL))
                {
                    positions.Add(index + 1); // Note: change to 1 based position
                }
            }
        }

        private static string Concatenate(List<string> pages)
        {
            var stringBuilder = new StringBuilder();

            foreach (var page in pages)
            {
                stringBuilder.Append(page);
            }

            return stringBuilder.ToString();
        }

        private string Convert(List<int> positions)
        {
            if (positions.Count == 0)
            {
                positions.Add(0);
            }

            return string.Join(",", positions.Select(x => x.ToString()));
        }
    }
}
