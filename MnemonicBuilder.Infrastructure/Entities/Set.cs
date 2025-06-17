using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MnemonicBuilder.Infrastructure.Entities
{
    public class Set
    {
        public int Id { get; set; }
        public string Title { get; set; }  // Название набора
        public string Text { get; set; }  // Название набора
        public string? Description { get; set; }
        public DateTime CreatedAt { get; set; } = DateTime.UtcNow;

        // Привязка к пользователю
        public string UserId { get; set; }
        public User User { get; set; }

        public ICollection<SetSentence> SetSentences { get; set; }
    }
}
