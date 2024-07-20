namespace PaparaPatika.Entitities
{
    public class Author : Base
    {
        public string Name { get; set; }
        public string Surname { get; set; }
        public ICollection<Book> Books { get; set; }
    }
}
