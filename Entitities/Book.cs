using System.ComponentModel.DataAnnotations;

namespace PaparaPatika.Entitities
{
    public class Book : Base
    {
        [Required(ErrorMessage = "Kitap adı zorunludur.")]
        public string Title { get; set; }
        public DateTime PublishDate { get; set; }
        public Author Author { get; set; }
    }
}
