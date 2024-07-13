using PaparaPatika.Entitities;
using PaparaPatika.IRepositories;
using PaparaPatika.IServices;

namespace PaparaPatika.Services
{
    public class BookService : IBookService
    {
        private readonly IBookRepository _bookRepository;
        private readonly ILogger<BookService> _logger;

        public BookService(IBookRepository bookRepository, ILogger<BookService> logger)
        {
            _bookRepository = bookRepository;
            _logger = logger;
        }

        public async Task<Book> CreateBookAsync(Book newBook)
        {
            Book _newBook = await _bookRepository.CreateBookAsync(newBook);
            _logger.LogInformation("Kitap oluşturuldu: {Title}", newBook.Title);
            return _newBook;
        }

        public async Task<List<Book>> GetAllBooksAsync()
        {
            List<Book> books = await _bookRepository.GetAllBooksAsync();
            return books;
        }

        public async Task<Book> GetBookByIdAsync(int id)
        {
            Book? book = await _bookRepository.GetBookByIdAsync(id);
            return book;
        }

        public async Task<Book> RemoveBookAsync(int id)
        {
            Book bookToRemove = await _bookRepository.RemoveBookAsync(id);
            if (bookToRemove != null)
            {
                _logger.LogInformation("Kitap silindi: {Title}", bookToRemove.Title);
            }
            return bookToRemove;
        }

        public async Task<Book> UpdateBookAsync(Book newBook)
        {
            Book bookToUpdate = await _bookRepository.UpdateBookAsync(newBook);
            _logger.LogInformation("Kitap güncellendi: {Title}", newBook.Title);
            return bookToUpdate;
        }
    }
}
