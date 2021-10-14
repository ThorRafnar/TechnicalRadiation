using System.Collections.Generic;
using TechnicalRadiation.Models.Dtos;
using TechnicalRadiation.Models.InputModels;


namespace TechnicalRadiation.Repositories.Interfaces
{
    public interface INewsItemRepository
    {
        List<NewsItemDto> GetAllNewsItems();

        NewsItemDetailDto GetNewsItemById(int id);

        NewsItemDetailDto CreateNewsItem(NewsItemInputModel newsItem);
        void DeleteNewsItem(int id);
        void UpdateNewsItem(NewsItemInputModel news, int id);
        void PartiallyUpdateNewsItem(NewsItemInputModel news, int id);
        bool DoesExist(int id);
        void AddLinks(NewsItemDto news);
    }
}