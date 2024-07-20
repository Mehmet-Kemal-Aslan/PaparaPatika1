using Microsoft.EntityFrameworkCore;

namespace PaparaPatika.Entitities
{
    public class PaparaDbContext : DbContext
    {
        public PaparaDbContext(DbContextOptions options) : base(options)
        {
        }
        public DbSet<Book> Books { get; set; }
        public DbSet<Author> Authors { get; set; }
    }
}
