namespace MnemonicBuilder.Web.ViewModels
{
    public class CreateSentenceViewModel
    {
        public string Title { get; set; } = "";
        public string Description { get; set; } = "";
        public string Text { get; set; } = "";
        public bool IsPublic { get; set; } = false;
    }
}
