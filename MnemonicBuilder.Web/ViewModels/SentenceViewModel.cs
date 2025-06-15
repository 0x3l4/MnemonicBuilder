using System.ComponentModel.DataAnnotations;

namespace MnemonicBuilder.Web.ViewModels
{
    public class SentenceViewModel
    {
        public string Pattern { get; set; }
        public IEnumerable<string> Words { get; set; }
        public string Name { get; set; }
    }
}
