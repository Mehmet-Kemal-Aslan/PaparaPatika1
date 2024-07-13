using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using PaparaPatika.Entitities;
using PaparaPatika.IServices;

namespace PaparaPatika.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class BookController : ControllerBase
    {
        private readonly IBookService _bookService;

        public BookController(IBookService bookService)
        {
            _bookService = bookService;
        }

        [HttpGet]
        public async Task<IActionResult> GetAllBooksAsync() 
        {
            List<Book> bookList = await _bookService.GetAllBooksAsync();
            return Ok(bookList);
        }

        [HttpGet("id")]
        public async Task<IActionResult> GetBookByIdAsync([FromQuery] int id)
        {
            Book? book = await _bookService.GetBookByIdAsync(id);
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
            Book _newBook = await _bookService.CreateBookAsync(newBook);
            return Ok(_newBook);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateBookAsync([FromBody] Book bookToUpdate)
        {
            Book? _bookToUpdate = await _bookService.UpdateBookAsync(bookToUpdate);
            if (_bookToUpdate == null)
            {
                return NotFound();
            }
            return Ok(bookToUpdate);
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> RemoveBookAsync(int id)
        {
            Book bookToRemove = await _bookService.RemoveBookAsync(id);
            if (bookToRemove == null)
            {
                return NotFound();
            }
            return Ok();
        }

        //[HttpGet("sort")]
        //public async Task<IActionResult> SortBooksByNameAsync()
        //{
        //    List<Book> sortedBookList = await _context.Books.OrderBy(b => b.Title).ToListAsync();
        //    return Ok(sortedBookList);
        //}
    }
}
