using System.ComponentModel.DataAnnotations;

namespace PaparaPatika.Entitities
{
    public class Book
    {
        public int Id { get; set; }
        [Required(ErrorMessage = "Kitap adı zorunludur.")]
        public string Title { get; set; }
        public DateTime PublishDate { get; set; }

    }
}
