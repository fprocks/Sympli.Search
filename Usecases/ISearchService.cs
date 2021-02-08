using System.Threading.Tasks;
using Sympli.Search.Contracts.Responses;

namespace Sympli.Search.Usecases
{
    public interface ISearchService
    {
        Task<SearchResponse> Search(string keywords, string targetURL);
    }
}