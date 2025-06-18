using System.ComponentModel.DataAnnotations;

namespace MnemonicBuilder.Web.ViewModels
{
    public class CreateSetViewModel
    {
        [Required]
        public string Title { get; set; }
        public string? Description { get; set; }
        public bool IsPublic { get; set; }
        public List<SentenceItemViewModel> Sentences { get; set; } = new();
    }
}
