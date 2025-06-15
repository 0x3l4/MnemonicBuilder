using Microsoft.AspNetCore.Mvc;

namespace MnemonicBuilder.Web.Controllers
{
    public class FlashcardController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
