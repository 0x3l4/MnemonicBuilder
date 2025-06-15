using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MnemonicBuilder.Domain.Entities
{
    public class WordMatchResult
    {
        public List<string> Words { get; set; } = new();
        public bool HasMore { get; set; }
    }
}
