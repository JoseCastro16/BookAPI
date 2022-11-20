using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using LibraryAPI.Models;

namespace LibraryAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class BooksController : ControllerBase
    {
        private readonly LibraryAPIDBContext _context;

        public BooksController(LibraryAPIDBContext context)
        {
            _context = context;
        }

        // GET: api/Books
        [HttpGet]
        public async Task<ActionResult<BookResponse>> GetBooks()
        {
            var books = await _context.Books.Include(b => b.Author).ToListAsync();
            var responseObject = new BookResponse()
            {
                StatusCode =200,
                StatusDescription = "Successful. Able to retrieve all books",
                Books = books
            };

            return responseObject;
        }

        // GET: api/Books/5
        [HttpGet("{id}")]
        public async Task<ActionResult<BookResponse>> GetBook(int id)
        {
            var book = await _context.Books.Include(b => b.Author).FirstOrDefaultAsync(b => b.BookId == id); ;

            if (book == null)
            {
                return new BookResponse()
                {
                    StatusCode = 404,
                    StatusDescription = "Failed. Book not found."
                };
            }

            var bookList = new List<Book>
            {
                book
            };
            return new BookResponse()
            {
                StatusCode = 200,
                StatusDescription = "Successful. Book was found.",
                Books = bookList
            };
        }

        // PUT: api/Books/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<ActionResult<BookResponse>> PutBook(int id, Book book)
        {
            if (id != book.BookId)
            {
                return new BookResponse()
                {
                    StatusCode = 400,
                    StatusDescription = "Failed. Entered id does not match the book id"
                };
            }

            _context.Entry(book).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!BookExists(id))
                {
                    return new BookResponse()
                    {
                        StatusCode = 404,
                        StatusDescription = "Failed. Book does not exist."
                    };
                }
                else
                {
                    throw;
                }
            }

            return new BookResponse()
            {
                StatusCode = 204,
                StatusDescription = "Successful. Book was updated."
            };
        }

        // POST: api/Books
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<BookResponse>> PostBook(Book book)
        {
            _context.Books.Add(book);
            await _context.SaveChangesAsync();

            return new BookResponse()
            {
                StatusCode = 201,
                StatusDescription = "Successful. Book was created."
            };
        }

        // DELETE: api/Books/5
        [HttpDelete("{id}")]
        public async Task<ActionResult<BookResponse>> DeleteBook(int id)
        {
            var book = await _context.Books.FindAsync(id);
            if (book == null)
            {
                return new BookResponse()
                {
                    StatusCode = 404,
                    StatusDescription = "Failed. Book not found."
                };
            }

            _context.Books.Remove(book);
            await _context.SaveChangesAsync();

            return new BookResponse()
            {
                StatusCode = 204,
                StatusDescription = "Successful. Book was deleted."
            };
        }

        private bool BookExists(int id)
        {
            return _context.Books.Any(e => e.BookId == id);
        }
    }
}
