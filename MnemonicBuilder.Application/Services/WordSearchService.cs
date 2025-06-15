using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using MnemonicBuilder.Domain.Entities;
using MnemonicBuilder.Domain.Interfaces;

namespace MnemonicBuilder.Application.Services
{
    public class WordSearchService : IWordSearchService
    {
        private readonly IWordRepository _repository;

        public WordSearchService(IWordRepository repository)
        {
            _repository = repository;
        }

        public async Task<WordMatchResult> SearchWordsAsync(string pattern)
        {
            var result = new WordMatchResult();
            Regex regex;

            try
            {
                regex = new Regex(pattern, RegexOptions.IgnoreCase | RegexOptions.Compiled);
            }
            catch
            {
                throw new ArgumentException("Некорректное регулярное выражение.");
            }

            await foreach (var word in _repository.GetAllWordsAsync())
            {
                if (regex.IsMatch(word))
                    result.Words.Add(word);
            }

            return result;
        }
    }
}
