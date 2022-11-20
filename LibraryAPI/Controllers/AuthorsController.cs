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
    public class AuthorsController : ControllerBase
    {
        private readonly LibraryAPIDBContext _context;

        public AuthorsController(LibraryAPIDBContext context)
        {
            _context = context;
        }

        // GET: api/Authors
        [HttpGet]
        public async Task<ActionResult<AuthorResponse>> GetAuthors()
        {
            var authors = await _context.Authors.ToListAsync();

            var responseObject = new AuthorResponse()
            {
                StatusCode = 200,
                StatusDescription = "Successful. Able to retrieve all authors",
                Authors = authors
            };

            return responseObject;
        }

        // GET: api/Authors/5
        [HttpGet("{id}")]
        public async Task<ActionResult<AuthorResponse>> GetAuthor(int id)
        {
            var author = await _context.Authors.FindAsync(id);

            if (author == null)
            {
                return new AuthorResponse()
                {
                    StatusCode = 404,
                    StatusDescription="Failed. Author not found."
                };
            }
            var authorList = new List<Author>
            {
                author
            };
            return new AuthorResponse()
            {
                StatusCode = 200,
                StatusDescription = "Successful. Author was found.",
                Authors = authorList
            };
        }

        // PUT: api/Authors/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<ActionResult<AuthorResponse>> PutAuthor(int id, Author author)
        {
            if (id != author.AuthorId)
            {
                return new AuthorResponse()
                {
                    StatusCode=400,
                    StatusDescription="Failed. Entered id does not match the author id"
                };
            }

            _context.Entry(author).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!AuthorExists(id))
                {
                    return new AuthorResponse()
                    {
                        StatusCode = 404,
                        StatusDescription = "Failed. Author does not exist."
                    };
                }
                else
                {
                    throw;
                }
            }

            return new AuthorResponse()
            {
                StatusCode = 204,
                StatusDescription="Successful. Author was updated."
            };
        }

        // POST: api/Authors
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<AuthorResponse>> PostAuthor(Author author)
        {
            _context.Authors.Add(author);
            await _context.SaveChangesAsync();

            return new AuthorResponse()
            {
                StatusCode= 201,
                StatusDescription= "Successful. Author was created."
            };
        }

        // DELETE: api/Authors/5
        [HttpDelete("{id}")]
        public async Task<ActionResult<AuthorResponse>> DeleteAuthor(int id)
        {
            var author = await _context.Authors.FindAsync(id);
            if (author == null)
            {
                return new AuthorResponse()
                {
                    StatusCode = 404,
                    StatusDescription = "Failed. Author not found."
                };
            }

            _context.Authors.Remove(author);
            await _context.SaveChangesAsync();

            return new AuthorResponse()
            {
                StatusCode = 204,
                StatusDescription = "Successful. Author was deleted."
            };
        }

        private bool AuthorExists(int id)
        {
            return _context.Authors.Any(e => e.AuthorId == id);
        }
    }
}
