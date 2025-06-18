namespace MnemonicBuilder.Web.ViewModels
{
    public class SetEditViewModel
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public string? Description { get; set; }

        public List<SentenceItemViewModel> Sentences { get; set; } = new();
        public List<int> SelectedSentenceIds { get; set; } = new();
    }
}
