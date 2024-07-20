using AutoMapper;
using FluentValidation;
using PaparaPatika.Entitities;
using PaparaPatika.IRepositories;
using PaparaPatika.IServices;
using PaparaPatika.Repositories;
using PaparaPatika.Validation;
using PaparaPatika.ViewModels;

namespace PaparaPatika.Services
{
    public class AuthorService : IAuthorService
    {
        private readonly IGenericRepository<Author> _genericRepository;
        private readonly ILogger<AuthorService> _logger;
        private readonly IMapper _mapper;

        public AuthorService(IGenericRepository<Author> genericRepository, ILogger<AuthorService> logger, IMapper mapper)
        {
            _genericRepository = genericRepository;
            _logger = logger;
            _mapper = mapper;
        }

        public async Task<AuthorViewModel> CreateAuthorAsync(AuthorViewModel newAuthor)
        {
            AuthorValidator authorValidator = new AuthorValidator();
            var validationResult = await authorValidator.ValidateAsync(newAuthor);
            if (!validationResult.IsValid)
            {
                var errors = string.Join(", ", validationResult.Errors.Select(e => e.ErrorMessage));
                throw new ValidationException(errors);
            }
            Author authorToCreate = _mapper.Map<Author>(newAuthor);
            Author createdAuthor = await _genericRepository.Create(authorToCreate);
            AuthorViewModel _createdAuthor = _mapper.Map<AuthorViewModel>(createdAuthor);
            _logger.LogInformation("Yazar oluşturuldu: {Name} {Surname}", newAuthor.Name, newAuthor.Surname);
            return _createdAuthor;
        }

        public async Task<List<AuthorViewModel>> GetAllAuthorsAsync()
        {
            List<Author?> authors = await _genericRepository.GetAll();
            List<AuthorViewModel> authorList = _mapper.Map<List<AuthorViewModel>>(authors);
            return authorList;
        }

        public async Task<AuthorViewModel> GetAuthorByIdAsync(int id)
        {
            Author? author = await _genericRepository.GetById(id);
            AuthorViewModel _author = _mapper.Map<AuthorViewModel>(author);
            return _author;
        }

        public async Task<AuthorViewModel> RemoveAuthorAsync(int id)
        {
            Author? deletedAuthor = await _genericRepository.Delete(id);
            AuthorViewModel _deletedAuthor = _mapper.Map<AuthorViewModel>(deletedAuthor);
            if (deletedAuthor != null)
            {
                _logger.LogInformation("Yazar silindi: {Name} {Surname}", deletedAuthor.Name, deletedAuthor.Surname);
            }
            return _deletedAuthor;
        }

        public async Task<AuthorViewModel> UpdateAuthorAsync(int id, AuthorViewModel newAuthor)
        {
            AuthorValidator authorValidator = new AuthorValidator();
            var validationResult = await authorValidator.ValidateAsync(newAuthor);
            if (!validationResult.IsValid)
            {
                var errors = string.Join(", ", validationResult.Errors.Select(e => e.ErrorMessage));
                throw new ValidationException(errors);
            }
            Author authorToUpdate = _mapper.Map<Author>(newAuthor);
            authorToUpdate.Id = id;
            Author updatedAuthor = await _genericRepository.Update(authorToUpdate);
            AuthorViewModel _updatedauthor = _mapper.Map<AuthorViewModel>(updatedAuthor);
            _logger.LogInformation("Yazar güncellendi: {Name} {Surname}", newAuthor.Name, newAuthor.Surname);
            return _updatedauthor;
        }
    }
}
