using System.Collections.Generic;
using TechnicalRadiation.Models.Dtos;
using TechnicalRadiation.Models.InputModels;

namespace TechnicalRadiation.Repositories.Interfaces
{
    public interface IAuthorRepository
    {
        List<AuthorDto> GetAllAuthors();

        AuthorDetailDto GetAuthorById(int id);

        List<NewsItemDto> GetNewsItemsByAuthorId(int authodId);

        AuthorDetailDto CreateAuthor(AuthorInputModel authorIM);
        
        void ConnectAuthorAndNewsItem(int authId, int newsId);
        
        void DeleteAuthor(int id);
        void UpdateAuthor(AuthorInputModel author, int id);
        void PartiallyUpdateAuthor(AuthorInputModel author, int id);
        bool DoesExist(int id);
    }
}