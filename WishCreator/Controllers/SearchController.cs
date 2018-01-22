using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using WishCreator.Models;
using WishCreator.Services;

namespace WishCreator.Controllers
{
    [Route("api/[controller]")]
    public class SearchController : Controller
    {
        private ISearchService _searchService;

        public SearchController(ISearchService searchService)
        {
            _searchService = searchService;
        }

        [HttpGet]
        [Route("api/helloworld")]
        public string HelloWorld()
        {
            return "Hello World";
        }

        [HttpGet("{searchTerm}")]
        public SearchResult Get(string searchTerm)
        {
            return _searchService.Search(searchTerm);
        }
    }
}
