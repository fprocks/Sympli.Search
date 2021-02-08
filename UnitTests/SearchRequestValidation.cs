using FluentAssertions;
using Sympli.Search.Contracts.Requests;
using Sympli.Search.Usecases.Validators;
using Xunit;

namespace Sympli.Search.UnitTests
{
    public class SearchRequestValidation
    {
        [Fact]
        public void Should_validate_empty_search_request()
        {
            SearchRequest request = null;

            var validator = new SearchRequestValidator();
            var message = validator.Validate(request);

            message.Should().Be("Search request cannot be empty.");
        }

        [Theory]
        [InlineData(null, "sympli.com.au", "Keywords cannot be empty.")]
        [InlineData("e-settlements", null, "URL cannot be empty.")]
        public void Should_validate_search_keywords_and_url(string keywords, string url, string expected)
        {
            var request = new SearchRequest { Keywords = keywords, URL = url };

            var validator = new SearchRequestValidator();
            var actual = validator.Validate(request);

            actual.Should().Be(expected);
        }

        [Fact]
        public void Should_return_empty_message_for_valid_request()
        {
            var request = new SearchRequest { Keywords = "e-settlements", URL = "sympli.com.au" };

            var validator = new SearchRequestValidator();
            var message = validator.Validate(request);

            message.Should().BeNull();
        }
    }
}
