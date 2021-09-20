using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ECommerce.API.Search.Interfaces;
using ECommerce.API.Search.Models;
using Microsoft.AspNetCore.Mvc;

namespace ECommerce.API.Search.Controller
{
    [ApiController]
    [Route("api/search")]
    public class SearchController : ControllerBase
    {
        private readonly ISearchService _searchService;

        public SearchController(ISearchService service)
        {
            _searchService = service;
        }

        [HttpPost]
        public async Task<IActionResult> SearchAsync(SearchTerm terms) 
        {
            var results = await _searchService.SearchAsync(terms.CustomerId);
            if (results.IsSuccess) 
            {
                return Ok(results.SearchResult);
            }
            return NotFound();
        }
    }
}
