using BookAPI.Models;
using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace BookAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [EnableCors("CorsPolicy")]
    public class BooksController : ControllerBase
    {
        private readonly BooksDbContext _context;
        public BooksController(BooksDbContext context)
        {
            _context = context;
        }
        [HttpGet]
        public async Task<ActionResult<List<Book>>> GetBooks()
        {
            return Ok(await _context.Books.ToListAsync());
        }
        [HttpPost]
        public async Task<ActionResult<List<Book>>> PostBook(Book book)
        {
            if (book ==null)
            {
                return BadRequest("book not found");
            }
            _context.Books.Add(book);
            await _context.SaveChangesAsync();
            return Ok(await _context.Books.ToListAsync());
        }

        [HttpPut("{id}")]
        public async Task<ActionResult<List<Book>>> UpdateBook(int id,Book book)
        {
            var dbBook = await _context.Books.FindAsync(id);
            if (dbBook == null)
                return BadRequest("book not found!");
            dbBook.Title = book.Title;
            dbBook.Author = book.Author;
            dbBook.NumberOfPages= book.NumberOfPages;
            dbBook.PublishedAt = book.PublishedAt;
            await _context.SaveChangesAsync();
            return Ok(await _context.Books.ToListAsync());
        }
        [HttpDelete("{id}")]
        public async Task<ActionResult<List<Book>>> DeleteBook(int id)
        {
            var dbBook =  await _context.Books.FindAsync(id);
            if (dbBook == null)
                return BadRequest("Hero not found!");
            _context.Remove(dbBook);
            await _context.SaveChangesAsync();
            return Ok(await _context.Books.ToListAsync());
        }
    }

}
