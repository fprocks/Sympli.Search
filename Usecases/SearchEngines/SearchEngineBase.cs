using System;
using System.Collections.Generic;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using Microsoft.Extensions.Logging;
using Sympli.Search.Contracts.Responses;
using Sympli.Search.Usecases.HtmlParser;

namespace Sympli.Search.Usecases.SearchEngines
{
    public abstract class SearchEngineBase : ISearchEngine
    {
        private readonly IHttpClientFactory _httpClientFactory;
        private readonly IHtmlParser _htmlParser;
        private readonly ILogger<SearchEngineBase> _logger;

        public SearchEngineBase(
            IHttpClientFactory httpClientFactory,
            ILogger<SearchEngineBase> logger,
            IHtmlParser htmlParser)
        {
            _httpClientFactory = httpClientFactory;
            _logger = logger;
            _htmlParser = htmlParser;
        }

        public abstract string EngineName { get; }
        public abstract string EngineURL { get; }
        public abstract string MatchPattern { get; }

        protected abstract Task<List<string>> FetchPages(string keywords);
     
        public async Task<SearchResult> Search(string keywords, string targetURL)
        {
            try
            {
                var pages = await FetchPages(keywords);

                var positions = _htmlParser.Parse(pages, targetURL, MatchPattern);

                return new SearchResult(EngineName, positions);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, ex.Message, $"An error occurred while searching {keywords}");

                throw; // Rethrow so that exception middleware will process it further
            }
        }

        protected HttpClient CreateHttpClient()
        {
            var httpClient = _httpClientFactory.CreateClient();
            httpClient.DefaultRequestHeaders.Add("User-Agent", "Mozilla/5.0 (Windows NT 10.0; Win64; x64) AppleWebKit/537.36 (KHTML, like Gecko) Chrome/88.0.4324.146 Safari/537.36");
            return httpClient;
        }

        protected string ProcessKeywords(string keywordsStr)
        {
            var keywords = keywordsStr.Split(' ');
            return WebUtility.HtmlEncode(string.Join("+", keywords)) ;
        }
    }
}