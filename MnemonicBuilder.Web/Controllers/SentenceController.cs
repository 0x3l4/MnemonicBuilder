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
        public IActionResult Save()
        {
            return View();
        }
    }
}
