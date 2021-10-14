using System.Collections.Generic;
using TechnicalRadiation.Models.Dtos;
using TechnicalRadiation.Models.InputModels;

namespace TechnicalRadiation.Services.Interfaces
{
    public interface IAuthorService
    {
        AuthorDetailDto GetAuthorById(int id);
        List<AuthorDto> GetAllAuthors();

        List<NewsItemDto> GetNewsItemsByAuthorId(int authorId);

        AuthorDetailDto CreateAuthor(AuthorInputModel authorIM);

        void ConnectAuthorAndNewsItem(int authId, int newsId);
        
        void DeleteAuthor(int id);
        void UpdateAuthor(AuthorInputModel author, int id);
        void PartiallyUpdateAuthor(AuthorInputModel author, int id);
    }
}