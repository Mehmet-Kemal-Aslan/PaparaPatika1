using PaparaPatika.Entitities;

namespace PaparaPatika.IServices
{
    public interface IBookService
    {
        Task<List<Book>> GetAllBooksAsync();
        Task<Book> GetBookByIdAsync(int id);
        Task<Book> CreateBookAsync(Book newBook);
        Task<Book> UpdateBookAsync(Book newBook);
        Task<Book> RemoveBookAsync(int id);
    }
}
