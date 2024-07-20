using PaparaPatika.ViewModels;

namespace PaparaPatika.IServices
{
    public interface IAuthorService
    {
        Task<List<AuthorViewModel>> GetAllAuthorsAsync();
        Task<AuthorViewModel> GetAuthorByIdAsync(int id);
        Task<AuthorViewModel> CreateAuthorAsync(AuthorViewModel newAuthor);
        Task<AuthorViewModel> UpdateAuthorAsync(int id, AuthorViewModel newAuthor);
        Task<AuthorViewModel> RemoveAuthorAsync(int id);
    }
}
