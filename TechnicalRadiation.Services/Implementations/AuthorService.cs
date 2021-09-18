using System.Collections.Generic;
using TechnicalRadiation.Models.Dtos;
using TechnicalRadiation.Models.InputModels;
using TechnicalRadiation.Repositories.Interfaces;
using TechnicalRadiation.Services.Interfaces;

namespace TechnicalRadiation.Services.Implementations
{
    public class AuthorService : IAuthorService
    {
        private readonly IAuthorRepository _authorRepository;

        public AuthorService(IAuthorRepository authorRepository)
        {
            _authorRepository = authorRepository;
        }
        
        public AuthorDetailDto GetAuthorById(int id)
        {
            return _authorRepository.GetAuthorById(id);
        }

        public List<AuthorDto> GetAllAuthors()
        {
            return _authorRepository.GetAllAuthors();
        }

        public List<NewsItemDto> GetNewsItemsByAuthorId(int authorId)
        {
            return _authorRepository.GetNewsItemsByAuthorId(authorId);
        }

        public AuthorDetailDto CreateAuthor(AuthorInputModel authorIM)
        {
            return _authorRepository.CreateAuthor(authorIM);
        }

        public NewsItemDetailDto ConnectAuthorAndNewsItem(int authId, int newsId)
        {
            return _authorRepository.ConnectAuthorAndNewsItem(authId, newsId);
        }
    }
}