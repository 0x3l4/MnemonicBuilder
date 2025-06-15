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

        public async IAsyncEnumerable<string> GetAllWordsAsync()
        {
            using var reader = File.OpenText(_filePath);
            while (!reader.EndOfStream)
            {
                var line = await reader.ReadLineAsync();
                if (line != null)
                    yield return line;
            }
        }
    }
}
