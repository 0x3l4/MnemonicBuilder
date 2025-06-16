using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using MnemonicBuilder.Application.Services;
using MnemonicBuilder.Web.ViewModels;

namespace MnemonicBuilder.Web.Controllers
{
    public class SentenceController : Controller
    {
        private readonly WordSearchService _wordService;

        public SentenceController(WordSearchService wordService)
        {
            _wordService = wordService;
        }

        [HttpGet]
        public IActionResult Index()
        {
            return View();
        }

        [HttpGet]
        public IActionResult Create()
        {
            return View();
        }

        [HttpGet]
        public IActionResult Edit(int? id)
        {
            if (id <= 0 || id == null)
            {
                return NotFound();
            }

            

            return View();
        }

        // AJAX-экшен для поиска и пагинации (возвращает частичное представление)
        [HttpGet]
        public IActionResult Search(string pattern, int page = 1)
        {
            if (string.IsNullOrWhiteSpace(pattern))
                return BadRequest("Регулярное выражение не задано.");

            const int pageSize = 30;
            var (words, total) = _wordService.Search(pattern, page);

            Console.WriteLine($"Количество найденных слов: {words.Count}");

            var model = new WordSearchViewModel
            {
                Pattern = pattern,
                Words = words,
                CurrentPage = page,
                PageSize = pageSize,
                TotalCount = total
            };
            return PartialView("_SearchResults", model);
        }


        [HttpPost]
        public IActionResult Save()
        {
            return View();
        }
    }
}
