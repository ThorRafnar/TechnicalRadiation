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
        int DeleteNewsItem(int id);
    }
}