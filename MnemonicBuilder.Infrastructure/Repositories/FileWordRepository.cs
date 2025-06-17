using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using MnemonicBuilder.Domain.Interfaces;

namespace MnemonicBuilder.Infrastructure.Repositories
{
    public class FileWordRepository : IWordRepository
    {
        private readonly string _filePath;

        public FileWordRepository(string path)
        {
            _filePath = path;
        }

        public IEnumerable<string> SearchWords(string pattern, int skip, int take)
        {
            var regex = new Regex(pattern);
            // Читаем файл строка за строкой, фильтруем по regex и делаем Skip/Take для пагинации
            return File.ReadLines(_filePath)
                       .Where(word => regex.IsMatch(word))
                       .Skip(skip)
                       .Take(take);
        }

        public int CountWords(string pattern)
        {
            var regex = new Regex(pattern);

            return File.ReadLines(_filePath)
                       .Count(word => regex.IsMatch(word));
        }
    }
}
