using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using MnemonicBuilder.Application.Services;
using MnemonicBuilder.Domain.Interfaces;
using MnemonicBuilder.Web.Models;

namespace MnemonicBuilder.Web.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class WordSearchController : ControllerBase
    {
        private readonly IWordSearchService _searchService;
        private readonly WordSearchCacheService _cacheService;

        public WordSearchController(IWordSearchService searchService, WordSearchCacheService cacheService)
        {
            _searchService = searchService;
            _cacheService = cacheService;
        }

        [HttpPost("search")]
        public async Task<IActionResult> Search([FromBody] RegexSearchRequest request)
        {
            if (string.IsNullOrWhiteSpace(request.Pattern))
                return BadRequest("Шаблон пуст.");

            try
            {
                var result = await _searchService.SearchWordsAsync(request.Pattern);
                var cacheId = _cacheService.SaveWords(result.Words);
                return Ok(new RegexSearchResponse
                {
                    CacheId = cacheId,
                    Total = result.Words.Count
                });
            }
            catch (ArgumentException ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpGet("page")]
        public IActionResult GetPage([FromQuery] PageRequest request)
        {
            var list = _cacheService.GetWords(request.CacheId);
            if (list == null)
                return NotFound("Результат устарел или не найден.");

            var items = list
                .Skip((request.Page - 1) * request.PageSize)
                .Take(request.PageSize)
                .ToList();

            return Ok(new PageResponse
            {
                Page = request.Page,
                PageSize = request.PageSize,
                Total = list.Count,
                Items = items
            });
        }
    }
}
