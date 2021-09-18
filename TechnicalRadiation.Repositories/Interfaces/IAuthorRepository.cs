using System.Collections.Generic;
using TechnicalRadiation.Models.Dtos;
using TechnicalRadiation.Models.InputModels;

namespace TechnicalRadiation.Repositories.Interfaces
{
    public interface IAuthorRepository
    {
        public List<AuthorDto> GetAllAuthors();

        public AuthorDetailDto GetAuthorById(int id);

        public List<NewsItemDto> GetNewsItemsByAuthorId(int authodId);

        public AuthorDetailDto CreateAuthor(AuthorInputModel authorIM);
        
        public NewsItemDetailDto ConnectAuthorAndNewsItem(int authId, int newsId);
    }
}