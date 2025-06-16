using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using MnemonicBuilder.Domain.Interfaces;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore;
using Microsoft.Extensions.Hosting;

namespace MnemonicBuilder.Infrastructure.Repositories
{
    public class FileWordRepository : IWordRepository
    {
        private readonly string _filePath;

        public FileWordRepository(IWebHostEnvironment env)
        {
            // Предполагаем, что файл лежит в wwwroot/data
            _filePath = Path.Combine(env.WebRootPath, "data", "all_russian_words.txt");
        }

        public IEnumerable<string> SearchWords(string pattern, int skip, int take)
        {
            var regex = new Regex(pattern, RegexOptions.IgnoreCase);
            // Читаем файл строка за строкой, фильтруем по regex и делаем Skip/Take для пагинации
            return File.ReadLines(_filePath)
                       .Where(word => regex.IsMatch(word))
                       .Skip(skip)
                       .Take(take);
        }

        public int CountWords(string pattern)
        {
            var regex = new Regex(pattern, RegexOptions.IgnoreCase);
            // Подсчитываем совпадения (может быть медленно для огромного файла, 
            // но демонстративно показывает логику)
            return File.ReadLines(_filePath)
                       .Count(word => regex.IsMatch(word));
        }
    }
}
