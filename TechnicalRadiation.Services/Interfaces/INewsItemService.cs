using System.Collections.Generic;
using TechnicalRadiation.Models;
using TechnicalRadiation.Models.Dtos;
using TechnicalRadiation.Models.InputModels;

namespace TechnicalRadiation.Services.Interfaces
{
    public interface INewsItemService
    {
        NewsItemDetailDto GetNewsItemById(int id);
        Envelope<NewsItemDto> GetAllNewsItems(int pageNumber, int pageSize);

        NewsItemDetailDto CreateNewsItem(NewsItemInputModel newsItem);
        int DeleteNewsItem(int id);
        
    }
}