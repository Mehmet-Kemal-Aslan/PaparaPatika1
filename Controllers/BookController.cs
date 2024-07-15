using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using PaparaPatika.Entitities;
using PaparaPatika.IServices;
using PaparaPatika.Validation;
using PaparaPatika.ViewModels;
using System.ComponentModel.DataAnnotations;

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

        // Aşağıdaki metodlarda viewModeller kullanılmıştır.

        [HttpGet]
        public async Task<IActionResult> GetAllBooksAsync() 
        {
            List<BookViewModel> bookList = await _bookService.GetAllBooksAsync();
            return Ok(bookList);
        }

        [HttpGet("id")]
        public async Task<IActionResult> GetBookByIdAsync([FromQuery] int id)
        {
            BookViewModel? book = await _bookService.GetBookByIdAsync(id);
            if (book == null)
            {
                return NotFound();
            }
            return Ok(book);
        }

        // Gelen kayıt modelinin validasyon kontrolü de yapılıyor.
        [HttpPost]
        public async Task<IActionResult> CreateBookAsync([FromBody] BookViewModel newBook)
        {
            try
            {
                BookViewModel createdBook = await _bookService.CreateBookAsync(newBook);
                return Ok(createdBook);
            }
            catch (ValidationException ex)
            {
                return BadRequest(new { Errors = ex.Message });
            }
        }

        // Gelen güncelleme modelinin validasyon kontrolü de yapılıyor.
        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateBookAsync([FromRoute] int id, [FromBody] BookViewModel bookToUpdate)
        {
            try
            {
                BookViewModel? updatedBook = await _bookService.UpdateBookAsync(id, bookToUpdate);
                if (updatedBook == null)
                {
                    return NotFound();
                }
                return Ok(updatedBook);
            }
            catch(ValidationException ex)
            {
                return BadRequest(new { Errors = ex.Message });
            }
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> RemoveBookAsync(int id)
        {
            BookViewModel bookToRemove = await _bookService.RemoveBookAsync(id);
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
