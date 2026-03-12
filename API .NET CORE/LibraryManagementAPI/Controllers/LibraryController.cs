using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using LibraryManagementAPI.Data;
using LibraryManagementAPI.Models;

namespace LibraryManagementAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class LibraryController : ControllerBase
    {
        private readonly LibraryDbContext _context;

        public LibraryController(LibraryDbContext context)
        {
            _context = context;
        }

        // Display all books
        [HttpGet("books")]
        public async Task<IActionResult> DisplayAllBooks()
        {
            var books = await _context.Books
                .Select(x => new
                {
                    x.Id,
                    x.Title,
                    x.Author,
                    x.PublishedYear
                }).ToListAsync();

            return Ok(books);
        }

        // Add Book
        [HttpPost("add-book")]
        public async Task<IActionResult> AddBook([FromBody] Book book)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            _context.Books.Add(book);
            await _context.SaveChangesAsync();

            return CreatedAtAction(nameof(DisplayAllBooks), new { id = book.Id }, book);
        }

        // Books by Library Card
        [HttpGet("librarycard/{libraryCardId}/books")]
        public async Task<IActionResult> DisplayBooksForLibraryCard(int libraryCardId)
        {
            var books = await _context.Books
                .Where(x => x.LibraryCardId == libraryCardId)
                .Select(x => new
                {
                    x.Id,
                    x.Title,
                    x.Author,
                    x.PublishedYear
                }).ToListAsync();

            return Ok(books);
        }

        // Search by Title
        [HttpGet("search")]
        public async Task<IActionResult> SearchBooksByTitle(string query)
        {
            var books = await _context.Books
                .Where(x => x.Title.ToLower().Contains(query.ToLower()))
                .Select(x => new
                {
                    x.Id,
                    x.Title,
                    x.Author,
                    x.PublishedYear
                }).ToListAsync();

            return Ok(books);
        }
    }
}
