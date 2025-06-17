using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using MnemonicBuilder.Application.Services;
using MnemonicBuilder.Domain.Entities;
using MnemonicBuilder.Infrastructure.Data;
using MnemonicBuilder.Infrastructure.Entities;
using MnemonicBuilder.Web.ViewModels;

namespace MnemonicBuilder.Web.Controllers
{
    public class SentenceController : Controller
    {
        private readonly ApplicationDbContext _context;
        private readonly WordSearchService _wordService;
        private readonly UserManager<User> _userManager;
        public SentenceController(ApplicationDbContext context, UserManager<User> userManager, WordSearchService wordService)
        {
            _context = context;
            _wordService = wordService;
            _userManager = userManager;
        }

        [HttpGet]
        public IActionResult Index()
        {
            return View();
        }

        [HttpGet]
        public IActionResult Create()
        {
            return View(new CreateSentenceViewModel());
        }

        [Authorize]
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(CreateSentenceViewModel model)
        {
            if (!ModelState.IsValid)
                return View(model);

            var user = await _userManager.GetUserAsync(User);
            if (user == null)
            {
                return Forbid(); // пользователь не авторизован (подстраховка)
            }

            var sentence = new Sentence
            {
                Title = model.Title,
                Description = model.Description,
                Text = model.Text,
                IsPublic = model.IsPublic,
                UserId = user.Id
            };

            _context.Sentences.Add(sentence);
            await _context.SaveChangesAsync();

            return RedirectToAction("Index", "Home");
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
