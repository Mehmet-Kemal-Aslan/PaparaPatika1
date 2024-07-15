using AutoMapper;
using PaparaPatika.Entitities;
using PaparaPatika.IRepositories;
using PaparaPatika.IServices;
using PaparaPatika.ViewModels;

namespace PaparaPatika.Services
{
    public class BookService : IBookService
    {
        private readonly IBookRepository _bookRepository;
        private readonly ILogger<BookService> _logger;
        private readonly IMapper _mapper;

        public BookService(IBookRepository bookRepository, ILogger<BookService> logger, IMapper mapper)
        {
            _bookRepository = bookRepository;
            _logger = logger;
            _mapper = mapper;
        }

        public async Task<BookViewModel> CreateBookAsync(BookViewModel newBook)
        {
            Book bookToCreate = _mapper.Map<Book>(newBook);
            Book createdBook = await _bookRepository.CreateBookAsync(bookToCreate);
            BookViewModel _createdBook = _mapper.Map<BookViewModel>(createdBook);
            _logger.LogInformation("Kitap oluşturuldu: {Title}", newBook.Title);
            return _createdBook;
        }

        public async Task<List<BookViewModel>> GetAllBooksAsync()
        {
            List<Book> books = await _bookRepository.GetAllBooksAsync();
            List<BookViewModel> bookList = _mapper.Map<List<BookViewModel>>(books);
            return bookList;
        }

        public async Task<BookViewModel> GetBookByIdAsync(int id)
        {
            Book? book = await _bookRepository.GetBookByIdAsync(id);
            BookViewModel _book = _mapper.Map<BookViewModel>(book);
            return _book;
        }

        public async Task<BookViewModel> RemoveBookAsync(int id)
        {
            Book deletedBook = await _bookRepository.RemoveBookAsync(id);
            BookViewModel _deletedBook = _mapper.Map<BookViewModel>(deletedBook);
            if (deletedBook != null)
            {
                _logger.LogInformation("Kitap silindi: {Title}", deletedBook.Title);
            }
            return _deletedBook;
        }

        public async Task<BookViewModel> UpdateBookAsync(int id, BookViewModel newBook)
        {
            Book bookToUpdate = _mapper.Map<Book>(newBook);
            bookToUpdate.Id = id;
            Book updatedBook = await _bookRepository.UpdateBookAsync(bookToUpdate);
            BookViewModel _updatedBook = _mapper.Map<BookViewModel>(updatedBook);
            _logger.LogInformation("Kitap güncellendi: {Title}", newBook.Title);
            return _updatedBook;
        }
    }
}
