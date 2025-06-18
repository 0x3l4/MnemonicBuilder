using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using MnemonicBuilder.Infrastructure.Data;
using MnemonicBuilder.Infrastructure.Entities;
using MnemonicBuilder.Web.ViewModels;

namespace MnemonicBuilder.Web.Controllers
{
    [Authorize]
    public class SetController : Controller
    {
        private readonly ApplicationDbContext _context;
        private readonly UserManager<User> _userManager;

        public SetController(ApplicationDbContext context, UserManager<User> userManager)
        {
            _context = context;
            _userManager = userManager;
        }

        [HttpGet]
        public async Task<IActionResult> My()
        {
            var userId = _userManager.GetUserId(User);

            var sets = await _context.Sets
                .Where(s => s.UserId == userId)
                .OrderByDescending(s => s.CreatedAt)
                .ToListAsync();

            return View(sets);
        }

        [HttpGet]
        public async Task<IActionResult> Edit(int id)
        {
            var userId = _userManager.GetUserId(User);

            var set = await _context.Sets
                .Where(s => s.Id == id && s.UserId == userId)
                .FirstOrDefaultAsync();

            if (set == null)
                return NotFound();

            var sentences = await _context.Sentences
                .Where(s => s.UserId == userId)
                .ToListAsync();

            var selectedSentenceIds = await _context.SetSentences
                .Where(ss => ss.SetId == id)
                .Select(ss => ss.SentenceId)
                .ToListAsync();

            var model = new SetEditViewModel
            {
                Id = set.Id,
                Title = set.Title,
                Description = set.Description,
                Sentences = sentences.Select(s => new SentenceItemViewModel
                {
                    Id = s.Id,
                    Title = s.Title,
                    Text = s.Text,
                    IsSelected = selectedSentenceIds.Contains(s.Id)
                }).ToList()
            };

            return View(model);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(SetEditViewModel model)
        {
            var userId = _userManager.GetUserId(User);

            var set = await _context.Sets
                .Where(s => s.Id == model.Id && s.UserId == userId)
                .FirstOrDefaultAsync();

            if (set == null)
                return NotFound();

            set.Title = model.Title;
            set.Description = model.Description;

            // Обновляем связанные предложения
            var existingSentenceLinks = await _context.SetSentences
                .Where(ss => ss.SetId == set.Id)
                .ToListAsync();

            _context.SetSentences.RemoveRange(existingSentenceLinks);

            if (model.SelectedSentenceIds != null)
            {
                foreach (var sentenceId in model.SelectedSentenceIds)
                {
                    _context.SetSentences.Add(new SetSentence
                    {
                        SetId = set.Id,
                        SentenceId = sentenceId
                    });
                }
            }

            await _context.SaveChangesAsync();

            return RedirectToAction(nameof(My));
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Delete(int id)
        {
            var userId = _userManager.GetUserId(User);

            var set = await _context.Sets
                .Where(s => s.Id == id && s.UserId == userId)
                .FirstOrDefaultAsync();

            if (set == null)
                return NotFound();

            // Сначала удаляем связи в таблице SetSentences
            var setSentences = _context.SetSentences.Where(ss => ss.SetId == id);
            _context.SetSentences.RemoveRange(setSentences);

            // Потом сам Set
            _context.Sets.Remove(set);

            await _context.SaveChangesAsync();

            return RedirectToAction(nameof(My));
        }

        [HttpGet]
        public async Task<IActionResult> Details(int id)
        {
            var set = await _context.Sets
                .Include(s => s.SetSentences)
                    .ThenInclude(ss => ss.Sentence)
                .FirstOrDefaultAsync(s => s.Id == id && s.IsPublic);

            if (set == null)
                return NotFound();

            return View(set);
        }

        [HttpGet]
        public async Task<IActionResult> Public()
        {
            var publicSets = await _context.Sets
                .Where(s => s.IsPublic)
                .OrderByDescending(s => s.CreatedAt)
                .ToListAsync();

            return View(publicSets);
        }

        [HttpGet]
        public async Task<IActionResult> Create()
        {
            var sentences = await _context.Sentences
                .Where(s => s.UserId == _userManager.GetUserId(User))
                .Select(s => new SentenceItemViewModel
                {
                    Id = s.Id,
                    Title = s.Title,
                    Text = s.Text,
                    
                })
                .ToListAsync();

            var viewModel = new CreateSetViewModel
            {
                Sentences = sentences
            };

            return View(viewModel);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(CreateSetViewModel model)
        {
            if (!ModelState.IsValid)
            {
                return View(model);
            }

            var userId = _userManager.GetUserId(User);

            var set = new Set
            {
                Title = model.Title,
                Description = model.Description,
                UserId = userId,
                CreatedAt = DateTime.UtcNow,
                IsPublic = model.IsPublic,
            };

            // Добавляем выбранные предложения
            var selectedSentenceIds = model.Sentences
                .Where(s => s.IsSelected)
                .Select(s => s.Id)
                .ToList();

            set.SetSentences = selectedSentenceIds
                .Select(id => new SetSentence { SentenceId = id })
                .ToList();

            _context.Sets.Add(set);
            _context.SaveChanges();

            return RedirectToAction(nameof(My)); // создадим потом страничку просмотра наборов
        }
    }
}
