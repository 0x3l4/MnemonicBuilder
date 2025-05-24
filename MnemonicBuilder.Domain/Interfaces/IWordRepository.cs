using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MnemonicBuilder.Domain.Interfaces
{
    public interface IWordRepository
    {
        Task<IEnumerable<string>> GetAllWordsAsync();
    }
}
