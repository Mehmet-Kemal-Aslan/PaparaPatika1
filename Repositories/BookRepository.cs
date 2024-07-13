using Microsoft.EntityFrameworkCore;
using PaparaPatika.Entitities;
using PaparaPatika.IRepositories;

namespace PaparaPatika.Repositories
{
    public class BookRepository : IBookRepository
    {
        private readonly PaparaDbContext _context;

        public BookRepository(PaparaDbContext context)
        {
            _context = context;
        }

        public async Task<Book> CreateBookAsync(Book newBook)
        {
            await _context.Books.AddAsync(newBook);
            await _context.SaveChangesAsync();
            return newBook;
        }

        public async Task<List<Book>> GetAllBooksAsync()
        {
            return await _context.Books.ToListAsync();
        }

        public async Task<Book> GetBookByIdAsync(int id)
        {
            Book? book = await _context.Books.FindAsync(id);
            return book == null ? null : book;
        }

        public async Task<Book> RemoveBookAsync(int id)
        {
            Book? bookToRemove = await _context.Books.FindAsync(id);
            if (bookToRemove == null) 
            {
                return null;
            }
            _context.Books.Remove(bookToRemove);
            await _context.SaveChangesAsync();
            return bookToRemove;
        }

        public async Task<Book> UpdateBookAsync(Book newBook)
        {
            Book? bookToUpdate = await _context.Books.FindAsync(newBook.Id);
            if (bookToUpdate == null)
            {
                return bookToUpdate;
            }
            bookToUpdate.Title = newBook.Title;
            bookToUpdate.PublishDate = newBook.PublishDate;
            await _context.SaveChangesAsync();
            return bookToUpdate;
        }
    }
}
