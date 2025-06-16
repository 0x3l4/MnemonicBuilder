using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MnemonicBuilder.Domain.Interfaces
{
    public interface IWordRepository
    {
        IEnumerable<string> SearchWords(string pattern, int skip, int take);

        int CountWords(string pattern);
    }
}
