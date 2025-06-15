using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using MnemonicBuilder.Web.ViewModels;

namespace MnemonicBuilder.Web.Controllers
{
    public class SentenceController : Controller
    {
        [HttpGet]
        public IActionResult Index()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Index(string pattern)
        {
            //var result = await _handler.Handle(new SearchWordsByPatternQuery(pattern));

            //SentenceViewModel resultWords = new SentenceViewModel
            //{
            //    Pattern = pattern,
            //    Words = result
            //};

            //return View(resultWords);
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

        //[HttpPost]
        //public IActionResult Search([FromBody] Regex)
        //{
        //     return View();
        //}

        [HttpPost]
        public IActionResult Save()
        {
            return View();
        }
    }
}
