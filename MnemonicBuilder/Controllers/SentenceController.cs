using Microsoft.AspNetCore.Mvc;

namespace MnemonicBuilder.Controllers
{
    public class SentenceController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
