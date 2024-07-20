using AutoMapper;
using PaparaPatika.Entitities;
using PaparaPatika.IRepositories;
using PaparaPatika.IServices;
using PaparaPatika.Repositories;
using PaparaPatika.Validation;
using PaparaPatika.ViewModels;
using System.ComponentModel.DataAnnotations;

namespace PaparaPatika.Services
{
    public class BookService : IBookService
    {
        private readonly IGenericRepository<Book> _genericRepository;
        private readonly ILogger<BookService> _logger;
        private readonly IMapper _mapper;

        public BookService(IGenericRepository<Book> genericRepository, ILogger<BookService> logger, IMapper mapper)
        {
            _genericRepository = genericRepository;
            _logger = logger;
            _mapper = mapper;
        }

        // Aşağıdaki metodlarda viewModeller kullanılmıştır.

        // Gelen kayıt modelinin validasyon kontrolü de yapılıyor.
        public async Task<BookViewModel> CreateBookAsync(BookViewModel newBook)
        {
            BookValidator bookValidator = new BookValidator();
            var validationResult = await bookValidator.ValidateAsync(newBook);
            if (!validationResult.IsValid)
            {
                var errors = string.Join(", ", validationResult.Errors.Select(e => e.ErrorMessage));
                throw new ValidationException(errors);
            }
            Book bookToCreate = _mapper.Map<Book>(newBook);
            Book createdBook = await _genericRepository.Create(bookToCreate);
            BookViewModel _createdBook = _mapper.Map<BookViewModel>(createdBook);
            _logger.LogInformation("Kitap oluşturuldu: {Title}", newBook.Title);
            return _createdBook;
        }

        public async Task<List<BookViewModel>> GetAllBooksAsync()
        {
            List<Book?> books = await _genericRepository.GetAll();
            List<BookViewModel> bookList = _mapper.Map<List<BookViewModel>>(books);
            return bookList;
        }

        public async Task<BookViewModel> GetBookByIdAsync(int id)
        {
            Book? book = await _genericRepository.GetById(id);
            BookViewModel _book = _mapper.Map<BookViewModel>(book);
            return _book;
        }

        public async Task<BookViewModel> RemoveBookAsync(int id)
        {
            Book deletedBook = await _genericRepository.Delete(id);
            BookViewModel _deletedBook = _mapper.Map<BookViewModel>(deletedBook);
            if (deletedBook != null)
            {
                _logger.LogInformation("Kitap silindi: {Title}", deletedBook.Title);
            }
            return _deletedBook;
        }

        // Gelen kayıt modelinin validasyon kontrolü de yapılıyor.
        public async Task<BookViewModel> UpdateBookAsync(int id, BookViewModel newBook)
        {
            BookValidator bookValidator = new BookValidator();
            var validationResult = await bookValidator.ValidateAsync(newBook);
            if (!validationResult.IsValid)
            {
                var errors = string.Join(", ", validationResult.Errors.Select(e => e.ErrorMessage));
                throw new ValidationException(errors);
            }
            Book bookToUpdate = _mapper.Map<Book>(newBook);
            bookToUpdate.Id = id;
            Book updatedBook = await _genericRepository.Update(bookToUpdate);
            BookViewModel _updatedBook = _mapper.Map<BookViewModel>(updatedBook);
            _logger.LogInformation("Kitap güncellendi: {Title}", newBook.Title);
            return _updatedBook;
        }
    }
}
