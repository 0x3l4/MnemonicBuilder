using Microsoft.AspNetCore.Mvc;

namespace MnemonicBuilder.Web.Controllers
{
    public class FlashcardController : Controller
    {
        public IActionResult My()
        {
            return View();
        }
    }
}
