using MnemonicBuilder.Infrastructure.Entities;

namespace MnemonicBuilder.Web.ViewModels
{
    public class EditSentenceViewModel
    {
        public string Title { get; set; } = "";
        public string Description { get; set; } = "";
        public string Text { get; set; } = "";
        public bool IsPublic { get; set; } = false;
    }
}
