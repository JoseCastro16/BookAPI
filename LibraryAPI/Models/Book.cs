using System;

namespace LibraryAPI.Models
{
    public class Book
    {
        public int BookId { get; set; }
        public string? Title { get; set; }
        public int? Authorid { get; set; }
        public Author? Author { get; set; }

    }
}
