using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MnemonicBuilder.Domain.Interfaces;

namespace MnemonicBuilder.Infrastructure.Repositories
{
    public class TextFileWordRepository : IWordRepository
    {
        public readonly string _filePath;

        public TextFileWordRepository(string filePath)
        {
            _filePath = filePath;
        }

        public async Task<IEnumerable<string>> GetAllWordsAsync()
        {
            if (!File.Exists(_filePath))
            {
                return Enumerable.Empty<string>();
            }

            var lines = await File.ReadAllLinesAsync(_filePath);

            return lines;
        }
    }
}
