using System.Text.Json;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.VisualStudio.Web.CodeGenerators.Mvc.Templates.BlazorIdentity.Pages;
using MnemonicBuilder.Application.Services;
using MnemonicBuilder.Infrastructure.Data;
using MnemonicBuilder.Infrastructure.Entities;
using MnemonicBuilder.Web.Extensions;
using MnemonicBuilder.Web.ViewModels;

namespace MnemonicBuilder.Web.Controllers
{
    public class SentenceController : Controller
    {
        private const string SentenceFormSessionKey = "SentenceForm";
        private readonly ApplicationDbContext _context;
        private readonly WordSearchService _wordService;
        private readonly UserManager<User> _userManager;
        public SentenceController(ApplicationDbContext context, UserManager<User> userManager, WordSearchService wordService)
        {
            _context = context;
            _wordService = wordService;
            _userManager = userManager;
        }

        [Authorize]
        [HttpGet]
        public async Task<IActionResult> My()
        {
            var user = await _userManager.GetUserAsync(User);
            if (user == null)
            {
                return Challenge(); // Если по какой-то причине авторизация сломалась
            }

            var mySentences = await _context.Sentences
                .Where(s => s.UserId == user.Id)
                .OrderByDescending(s => s.CreatedAt) // например, по дате создания
                .ToListAsync();

            return View(mySentences);
        }

        [HttpGet]
        public async Task<IActionResult> Public()
        {
            var publicSentences = await _context.Sentences
                .Where(s => s.IsPublic)
                .OrderByDescending(s => s.CreatedAt)
                .ToListAsync();

            return View(publicSentences);
        }

        [HttpGet]
        public IActionResult Create()
        {
            var model = HttpContext.Session.Get<CreateSentenceViewModel>(SentenceFormSessionKey)
                ?? new CreateSentenceViewModel();

            return View(model);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(CreateSentenceViewModel model)
        {
            if (!ModelState.IsValid)
            {
                HttpContext.Session.Set(SentenceFormSessionKey, model);
                return View(model);
            }

            var user = await _userManager.GetUserAsync(User);
            if (user == null)
            {
                HttpContext.Session.Set(SentenceFormSessionKey, model);
                return RedirectToAction("Login", "Account", new { returnUrl = Url.Action("Create", "Sentence") });
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

            HttpContext.Session.Remove(SentenceFormSessionKey);

            return RedirectToAction("My", "Sentence");
        }

        [HttpGet]
        public async Task<IActionResult> Edit(int? id)
        {
            var user = await _userManager.GetUserAsync(User);
            var sentence = await _context.Sentences
                .FirstOrDefaultAsync(s => s.Id == id && s.UserId == user.Id);

            if (sentence == null)
            {
                return NotFound();
            }

            return View(sentence);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, Sentence model)
        {
            var user = await _userManager.GetUserAsync(User);
            var sentence = await _context.Sentences
                .FirstOrDefaultAsync(s => s.Id == id && s.UserId == user.Id);

            if (sentence == null)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                sentence.Title = model.Title;
                sentence.Text = model.Text;
                sentence.Description = model.Description;
                sentence.IsPublic = model.IsPublic;
                sentence.CreatedAt = DateTime.UtcNow;

                _context.Update(sentence);
                await _context.SaveChangesAsync();

                return RedirectToAction(nameof(My));
            }

            return View(sentence);
        }

        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Delete(int id)
        {
            var user = await _userManager.GetUserAsync(User);
            var sentence = await _context.Sentences
                .FirstOrDefaultAsync(s => s.Id == id && s.UserId == user.Id);

            if (sentence == null)
            {
                return NotFound();
            }

            _context.Sentences.Remove(sentence);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(My));
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
