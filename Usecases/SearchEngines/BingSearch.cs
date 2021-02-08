using Microsoft.Extensions.Logging;
using Sympli.Search.Usecases.HtmlParser;
using System.Collections.Generic;
using System.Net.Http;
using System.Threading.Tasks;

namespace Sympli.Search.Usecases.SearchEngines
{
    public class BingSearch : SearchEngineBase
    {
        public BingSearch(
            IHttpClientFactory factory, 
            ILogger<SearchEngineBase> logger, 
            IHtmlParser htmlParser)
            : base(factory, logger, htmlParser)
        {
        }

        public override string EngineName => "Bing";
        public override string EngineURL => "https://www.bing.com/search?q={0}&count=50&first={1}";
        public override string MatchPattern => "<a href=\"http(s)?[a-zA-Z0-9--?=/]*\" h=";

        protected override async Task<List<string>> FetchPages(string keywords)
        {
            var httpClient = CreateHttpClient();

            var page1 = await httpClient.GetStringAsync(GetPageURL(keywords, 0));

            var page2 = await httpClient.GetStringAsync(GetPageURL(keywords, 50));

            return new List<string> { page1, page2 };
        }

        private string GetPageURL(string keywords, int offset)
        {
            return string.Format(EngineURL, ProcessKeywords(keywords), offset);
        }
    }
}