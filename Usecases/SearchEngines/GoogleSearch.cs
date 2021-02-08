using Microsoft.Extensions.Logging;
using Sympli.Search.Usecases.HtmlParser;
using System.Collections.Generic;
using System.Net.Http;
using System.Threading.Tasks;

namespace Sympli.Search.Usecases.SearchEngines
{
    public class GoogleSearch : SearchEngineBase
    {
        public GoogleSearch(
            IHttpClientFactory factory,
            ILogger<SearchEngineBase> logger,
            IHtmlParser htmlParser)
            :base(factory, logger, htmlParser)
        {
        }

        public override string EngineName => "Google";
        public override string EngineURL => "https://www.google.com/search?q={0}&num=100";
        public override string MatchPattern => "<a href=\"http(s)?[a-zA-Z0-9--?=/]*\" data-ved=";

        protected override async Task<List<string>> FetchPages(string keywords)
        {
            var url = string.Format(EngineURL, ProcessKeywords(keywords));

            var httpClient = CreateHttpClient();

            var page = await httpClient.GetStringAsync(url);

            return new List<string> { page };
        }
    }
}