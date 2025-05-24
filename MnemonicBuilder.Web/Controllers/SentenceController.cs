using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using MnemonicBuilder.Application.Words.Queries;
using MnemonicBuilder.Web.Models;

namespace MnemonicBuilder.Controllers
{
    public class SentenceController : Controller
    {
        private readonly SearchWordsByPatternHandler _handler;

        public SentenceController(SearchWordsByPatternHandler handler)
        {
            _handler = handler;
        }

        [HttpGet]
        public IActionResult Index()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Index(string pattern)
        {
            var result = await _handler.Handle(new SearchWordsByPatternQuery(pattern));

            ResultWordsViewModel resultWords = new ResultWordsViewModel
            {
                Pattern = pattern,
                Words = result
            };

            return View(resultWords);
        }
    }
}
