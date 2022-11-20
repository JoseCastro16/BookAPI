namespace LibraryAPI.Models
{
    public class BookResponse
    {
        public int StatusCode { get; set; }

        public string? StatusDescription { get; set; }

        public List<Book>? Books { get; set; }
    }
}
