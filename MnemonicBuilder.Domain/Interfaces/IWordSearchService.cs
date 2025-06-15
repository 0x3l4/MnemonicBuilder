using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MnemonicBuilder.Domain.Entities;

namespace MnemonicBuilder.Domain.Interfaces
{
    public interface IWordSearchService
    {
        Task<WordMatchResult> SearchWordsAsync(string pattern);
    }
}
