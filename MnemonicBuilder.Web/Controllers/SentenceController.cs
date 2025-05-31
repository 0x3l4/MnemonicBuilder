using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using MnemonicBuilder.Application.Words.Queries;
using MnemonicBuilder.Web.Models;

namespace MnemonicBuilder.Web.Controllers
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

            SentenceViewModel resultWords = new SentenceViewModel
            {
                Pattern = pattern,
                Words = result
            };

            return View(resultWords);
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

        [HttpPost]
        public IActionResult Search(string pattern)
        {
             return View();
        }

        [HttpPost]
        public IActionResult Save()
        {
            return View();
        }
    }
}
