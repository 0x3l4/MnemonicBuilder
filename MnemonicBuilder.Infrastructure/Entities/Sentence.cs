using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MnemonicBuilder.Domain.Entities;
using MnemonicBuilder.Infrastructure.Entities;

namespace MnemonicBuilder.Domain.Entities
{
    public class Sentence
    {
        public int Id { get; set; }

        public string Title { get; set; } = string.Empty;

        public string Description { get; set; } = string.Empty;

        public string Text { get; set; } = string.Empty;
        public bool IsPublic { get; set; } = false;
        public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
        public string UserId { get; set; } = string.Empty;

        public User? User { get; set; }
    }
}
