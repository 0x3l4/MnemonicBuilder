using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Extensions.Caching.Memory;

namespace MnemonicBuilder.Application.Services
{
    public class WordSearchCacheService
    {
        private readonly IMemoryCache _cache;
        private readonly TimeSpan _cacheDuration = TimeSpan.FromMinutes(10);

        public WordSearchCacheService(IMemoryCache cache)
        {
            _cache = cache;
        }

        public string SaveWords(List<string> words)
        {
            var id = Guid.NewGuid().ToString();
            _cache.Set(id, words, _cacheDuration);
            return id;
        }

        public List<string>? GetWords(string id)
        {
            return _cache.TryGetValue(id, out List<string>? words) ? words : null;
        }
    }
}
