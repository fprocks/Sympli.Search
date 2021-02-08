using Microsoft.AspNetCore.Mvc;
using Sympli.Search.Contracts.Requests;
using System.Threading.Tasks;
using Sympli.Search.Usecases;
using Sympli.Search.Usecases.Validators;
using Sympli.Search.Contracts.Responses;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace Sympli.Search.WebApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class SearchController : ControllerBase
    {
        private readonly ISearchService _searchService;
        private readonly ISearchRequestValidator _validator;

        public SearchController(ISearchService searchService, ISearchRequestValidator validator)
        {
            _searchService = searchService;
            _validator = validator;
        }

        [HttpGet]
        [ProducesResponseType(typeof(SearchResponse), 200)]
        [ProducesResponseType(400)]
        [ProducesResponseType(500)]
        public async Task<IActionResult> Get([FromQuery] SearchRequest request)
        {
            var message = _validator.Validate(request);
            if (message != null)
            {
                return BadRequest(message);
            }

            var response = await _searchService.Search(request.Keywords, request.URL);

            return Ok(response);
        }        
    }
}
