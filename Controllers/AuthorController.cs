using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using PaparaPatika.IServices;
using PaparaPatika.ViewModels;
using System.ComponentModel.DataAnnotations;

namespace PaparaPatika.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthorController : ControllerBase
    {
        private readonly IAuthorService _authorService;

        public AuthorController(IAuthorService authorService)
        {
            _authorService = authorService;
        }

        // Aşağıdaki metodlarda viewModeller kullanılmıştır.

        [HttpGet]
        public async Task<IActionResult> GetAllAuthorsAsync()
        {
            List<AuthorViewModel> authorList = await _authorService.GetAllAuthorsAsync();
            return Ok(authorList);
        }

        [HttpGet("id")]
        public async Task<IActionResult> GetAuthorByIdAsync([FromQuery] int id)
        {
            AuthorViewModel? author = await _authorService.GetAuthorByIdAsync(id);
            if (author == null)
            {
                return NotFound();
            }
            return Ok(author);
        }

        // Gelen kayıt modelinin validasyon kontrolü de yapılıyor.
        [HttpPost]
        public async Task<IActionResult> CreateAuthorAsync([FromBody] AuthorViewModel newAuthor)
        {
            try
            {
                AuthorViewModel createdAuthor = await _authorService.CreateAuthorAsync(newAuthor);
                return Ok(createdAuthor);
            }
            catch (ValidationException ex)
            {
                return BadRequest(new { Errors = ex.Message });
            }
            catch (Exception ex)
            {
                return StatusCode(500, "An unexpected error occurred.");
            }
        }

        // Gelen güncelleme modelinin validasyon kontrolü de yapılıyor.
        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateBookAsync([FromRoute] int id, [FromBody] AuthorViewModel authorToUpdate)
        {
            try
            {
                AuthorViewModel? updatedAuthor = await _authorService.UpdateAuthorAsync(id, authorToUpdate);
                if (updatedAuthor == null)
                {
                    return NotFound();
                }
                return Ok(updatedAuthor);
            }
            catch (ValidationException ex)
            {
                return BadRequest(new { Errors = ex.Message });
            }
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> RemoveBookAsync(int id)
        {
            AuthorViewModel authorToRemove = await _authorService.RemoveAuthorAsync(id);
            if (authorToRemove == null)
            {
                return NotFound();
            }
            return Ok();
        }
    }
}
