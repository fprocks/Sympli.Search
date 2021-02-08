namespace Sympli.Search.Contracts.Responses
{
    public class SearchResult
    {
        public SearchResult(string engineName, string positions)
        {
            EngineName = engineName;
            Positions = positions;
        }

        public string EngineName { get; set; }
        public string Positions { get; set; }
    }
}