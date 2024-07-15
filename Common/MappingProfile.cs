using AutoMapper;
using PaparaPatika.Entitities;
using PaparaPatika.ViewModels;

namespace PaparaPatika.Common
{
    public class MappingProfile : Profile
    {
        public MappingProfile()
        {
            CreateMap<Book, BookViewModel>();
            CreateMap<BookViewModel, Book>();
        }
    }
}
