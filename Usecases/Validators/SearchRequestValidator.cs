using Sympli.Search.Contracts.Requests;
using System;
using System.Collections.Generic;
using System.Text;

namespace Sympli.Search.Usecases.Validators
{
    public class SearchRequestValidator : ISearchRequestValidator
    {
        public string Validate(SearchRequest request)
        {
           if (request == null)
            {
                return "Search request cannot be empty.";
            }

            if (string.IsNullOrEmpty(request.Keywords))
            {
                return $"{nameof(request.Keywords)} cannot be empty.";
            }

            if (string.IsNullOrEmpty(request.URL))
            {
                return $"{nameof(request.URL)} cannot be empty.";
            }

            return null;
        }
    }

    public interface ISearchRequestValidator
    {
        string Validate(SearchRequest searchRequest);
    }
}
