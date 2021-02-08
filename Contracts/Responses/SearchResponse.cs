using System.Collections.Generic;

namespace Sympli.Search.Contracts.Responses
{
    public class SearchResponse
    {
        public SearchResponse()
        {
            Results = new List<SearchResult>();
        }
        
        public List<SearchResult> Results { get; set; }
    }
}
