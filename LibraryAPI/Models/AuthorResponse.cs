namespace LibraryAPI.Models
{
    public class AuthorResponse
    {
        public int StatusCode { get; set; }

        public string? StatusDescription { get; set; }

        public List<Author>? Authors { get; set; }
    }
}
