using System.Collections.Generic;

namespace Sympli.Search.Usecases.HtmlParser
{
    public interface IHtmlParser
    {
        string Parse(List<string> pages, string targetURL, string patternPrefix);
    }
}
