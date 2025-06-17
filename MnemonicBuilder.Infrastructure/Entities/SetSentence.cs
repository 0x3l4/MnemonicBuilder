using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MnemonicBuilder.Infrastructure.Entities
{
    public class SetSentence
    {
        public int SetId { get; set; }
        public Set Set { get; set; }

        public int SentenceId { get; set; }
        public Sentence Sentence { get; set; }
    }

}
