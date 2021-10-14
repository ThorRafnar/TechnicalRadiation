using System;
using System.Collections.Generic;
using TechnicalRadiation.Models.Dtos;
using TechnicalRadiation.Models.InputModels;
using TechnicalRadiation.Models.Exceptions;
using TechnicalRadiation.Repositories.Interfaces;
using TechnicalRadiation.Services.Interfaces;

namespace TechnicalRadiation.Services.Implementations
{
    public class AuthorService : IAuthorService
    {
        private readonly IAuthorRepository _authorRepository;
        private readonly INewsItemRepository _newsItemRepository;

        public AuthorService(IAuthorRepository authorRepository, INewsItemRepository newsItemRepository)
        {
            _authorRepository = authorRepository;
            _newsItemRepository = newsItemRepository;
        }
        
        public AuthorDetailDto GetAuthorById(int id)
        {
            if (id < 1) { throw new ArgumentOutOfRangeException(); }
            if (!_authorRepository.DoesExist(id)) { throw new ResourceNotFoundException($"Author with id {id} was not found."); }
            return _authorRepository.GetAuthorById(id);
        }

        public List<AuthorDto> GetAllAuthors()
        {
            return _authorRepository.GetAllAuthors();
        }

        public List<NewsItemDto> GetNewsItemsByAuthorId(int authorId)
        {
            if (authorId < 1) { throw new ArgumentOutOfRangeException(); }
            return _authorRepository.GetNewsItemsByAuthorId(authorId);
        }

        public AuthorDetailDto CreateAuthor(AuthorInputModel authorIM)
        {
            return _authorRepository.CreateAuthor(authorIM);
        }

        public void ConnectAuthorAndNewsItem(int authId, int newsId)
        {
            if (authId < 1 | newsId < 1) { throw new ArgumentOutOfRangeException(); }
            if (!_authorRepository.DoesExist(authId)) { throw new ResourceNotFoundException($"Author with id {authId} was not found."); }
            if (!_newsItemRepository.DoesExist(newsId)) { throw new ResourceNotFoundException($"News item with id {newsId} was not found."); }
            _authorRepository.ConnectAuthorAndNewsItem(authId, newsId);
            return;
        }

        public void DeleteAuthor(int id)
        {
            if (id < 1) { throw new ArgumentOutOfRangeException(); }
            if (!_authorRepository.DoesExist(id)) { throw new ResourceNotFoundException($"Author with id {id} was not found, and cannot be deleted."); }
            _authorRepository.DeleteAuthor(id);
            return;
        }

        public void UpdateAuthor(AuthorInputModel author, int id)
        {
            if (id < 1) { throw new ArgumentOutOfRangeException(); }
            if (!_authorRepository.DoesExist(id)) { throw new ResourceNotFoundException($"Author with id {id} was not found."); }
            _authorRepository.UpdateAuthor(author, id);
            return;
        }

        public void PartiallyUpdateAuthor(AuthorInputModel author, int id)
        {
            if (id < 1) { throw new ArgumentOutOfRangeException(); }
            if (!_authorRepository.DoesExist(id)) { throw new ResourceNotFoundException($"Author with id {id} was not found."); }
            _authorRepository.PartiallyUpdateAuthor(author, id);
            return;
        }
    }
}