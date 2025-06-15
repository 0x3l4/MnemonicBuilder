namespace MnemonicBuilder.Web.Models
{
    public class PageResponse
    {
        public int Page { get; set; }
        public int PageSize { get; set; }
        public int Total { get; set; }
        public List<string> Items { get; set; } = new();
    }
}
