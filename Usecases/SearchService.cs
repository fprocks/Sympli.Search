using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Sympli.Search.Contracts.Responses;
using Sympli.Search.Usecases.Cache;
using Sympli.Search.Usecases.SearchEngines;

namespace Sympli.Search.Usecases
{
    public class SearchService : ISearchService
    {
        private readonly IEnumerable<ISearchEngine> _searchEngines;
        private readonly ICache _cache;

        public SearchService(IEnumerable<ISearchEngine> searchEngines, ICache cache)
        {
            _searchEngines = searchEngines;
            _cache = cache;
        }

        public async Task<SearchResponse> Search(string keywords, string targetURL)
        {
            var results = new List<SearchResult>();
            foreach (var searchEngine in _searchEngines)
            {
                var cacheKey = GetCacheKey(keywords, targetURL, searchEngine.EngineName);

                var cachedResult = _cache.Get<SearchResult>(cacheKey);
                if (cachedResult != null)
                {
                    results.Add(cachedResult);
                }
                else
                {
                    var result = await searchEngine.Search(keywords, targetURL);

                    _cache.Set<SearchResult>(cacheKey, result, TimeSpan.FromMinutes(60));

                    results.Add(result);
                }
            }

            return new SearchResponse {Results = results};
        }

        public static string GetCacheKey(string keyword, string targetURL, string engineName)
        {
            return $"{keyword}-{targetURL}-{engineName}".ToLower();
        }
    }
}