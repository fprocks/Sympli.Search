using System.Collections.Generic;
using System.Threading.Tasks;
using Sympli.Search.Contracts.Responses;

namespace Sympli.Search.Usecases.SearchEngines
{
    public interface ISearchEngine
    {
        string EngineName { get; }
        string EngineURL { get; }
        string MatchPattern { get; }

        Task<SearchResult> Search(string keywords, string targetURL);
    }
}