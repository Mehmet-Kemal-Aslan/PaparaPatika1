using PaparaPatika.Entitities;
using PaparaPatika.ViewModels;

namespace PaparaPatika.IServices
{
    public interface IBookService
    {
        Task<List<BookViewModel>> GetAllBooksAsync();
        Task<BookViewModel> GetBookByIdAsync(int id);
        Task<BookViewModel> CreateBookAsync(BookViewModel newBook);
        Task<BookViewModel> UpdateBookAsync(int id, BookViewModel newBook);
        Task<BookViewModel> RemoveBookAsync(int id);
    }
}
