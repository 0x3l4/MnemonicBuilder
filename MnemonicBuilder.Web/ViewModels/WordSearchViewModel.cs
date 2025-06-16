namespace MnemonicBuilder.Web.ViewModels
{
    public class WordSearchViewModel
    {
        public string Pattern { get; set; }      // введённое регулярное выражение
        public List<string> Words { get; set; }  // найденные слова на текущей странице
        public int CurrentPage { get; set; }     // текущая страница
        public int PageSize { get; set; }        // размер страницы (например, 30)
        public int TotalCount { get; set; }      // общее количество найденных слов
    }
}
