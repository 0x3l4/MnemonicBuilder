using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MnemonicBuilder.Domain.Interfaces;

namespace MnemonicBuilder.Application.Services
{
    public class WordSearchService
    {
        private readonly IWordRepository _repo;
        private const int PageSize = 30;

        public WordSearchService(IWordRepository repo)
        {
            _repo = repo;
        }

        // Выполняет поиск по регулярному выражению pattern на запрошенной странице.
        public (List<string> Words, int TotalCount) Search(string pattern, int page = 1)
        {
            if (string.IsNullOrWhiteSpace(pattern))
                return (new List<string>(), 0);

            int skip = (page - 1) * PageSize;
            // Получаем общее количество совпадений
            int total = _repo.CountWords(pattern);
            // Получаем слова для текущей страницы
            var results = _repo.SearchWords(pattern, skip, PageSize).ToList();
            return (results, total);
        }
    }
}
