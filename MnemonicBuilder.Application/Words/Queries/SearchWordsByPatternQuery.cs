using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using MnemonicBuilder.Domain.Interfaces;

namespace MnemonicBuilder.Application.Words.Queries
{
    public record class SearchWordsByPatternQuery(string Pattern);

	public class SearchWordsByPatternHandler
	{
        private readonly IWordRepository _wordRepository;

        public SearchWordsByPatternHandler(IWordRepository wordRepository)
        {
            _wordRepository = wordRepository;
        }

        public async Task<IEnumerable<string>> Handle(SearchWordsByPatternQuery query)
        {
            var allWords = await _wordRepository.GetAllWordsAsync();

            try
            {
                var regex = new Regex(query.Pattern, RegexOptions.IgnoreCase);
                return allWords.Where(word =>  regex.IsMatch(word));
            }
            catch (Exception)
            {
                return Enumerable.Empty<string>();
            }
        }
    }
}
