namespace MnemonicBuilder.Web.Models
{
    public class PageRequest
    {
        public string CacheId { get; set; } = string.Empty;
        public int Page { get; set; } = 1;
        public int PageSize { get; set; } = 50;
    }
}
