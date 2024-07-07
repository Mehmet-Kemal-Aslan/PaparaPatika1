using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using PaparaPatika.Entitities;

namespace PaparaPatika.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class BookController : ControllerBase
    {
        private readonly PaparaDbContext _context;

        public BookController(PaparaDbContext context)
        {
            _context = context;
        }

        [HttpGet]
        public async Task<IActionResult> GetAllBooksAsync() 
        {
            List<Book> bookList = await _context.Books.ToListAsync();
            return Ok(bookList);
        }

        [HttpGet("id")]
        public async Task<IActionResult> GetBookByIdAsync([FromQuery] int id)
        {
            Book? book = await _context.Books.FindAsync(id);
            if (book == null)
            {
                return NotFound();
            }
            return Ok(book);
        }

        [HttpPost]
        public async Task<IActionResult> CreateBookAsync([FromBody] Book newBook)
        {
            if(!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            _context.Books.AddAsync(newBook);
            await _context.SaveChangesAsync();
            return Ok(newBook);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateBookAsync([FromBody] Book bookToUpdate)
        {
            Book? _bookToUpdate = await _context.Books.FindAsync(bookToUpdate.Id);
            if (_bookToUpdate == null)
            {
                return NotFound();
            }
            _bookToUpdate.Title = bookToUpdate.Title;
            _bookToUpdate.PublishDate = bookToUpdate.PublishDate;
            await _context.SaveChangesAsync();
            return Ok(bookToUpdate);
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> RemoveBookAsync(int id)
        {
            Book? bookToDelete = await _context.Books.FindAsync(id);
            if (bookToDelete == null)
            {
                return NotFound();
            }
            _context.Books.Remove(bookToDelete);
            await _context.SaveChangesAsync();
            return Ok();
        }

        [HttpGet("sort")]
        public async Task<IActionResult> SortBooksByNameAsync()
        {
            List<Book> sortedBookList = await _context.Books.OrderBy(b => b.Title).ToListAsync();
            return Ok(sortedBookList);
        }
    }
}
