using System;
using System.Collections.Generic;
using TechnicalRadiation.Models;
using TechnicalRadiation.Models.Dtos;
using TechnicalRadiation.Models.InputModels;
using TechnicalRadiation.Repositories.Interfaces;
using TechnicalRadiation.Services.Interfaces;

namespace TechnicalRadiation.Services.Implementations
{
    public class NewsItemService : INewsItemService
    {
        private readonly INewsItemRepository _newsItemRepository;

        public NewsItemService(INewsItemRepository newsItemRepository)
        {
            _newsItemRepository = newsItemRepository;
        }

        public NewsItemDetailDto GetNewsItemById(int id)
        {
            if (id < 1) { throw new ArgumentOutOfRangeException("Id should not be lower than 1"); }
            //if (!_newsItemRepository.DoesExist(id))
            //{
                // TODO: Implement and uncomment
                // throw new ResourceNotFoundException($"News item with id {id} was not found."); 
            //}
            return _newsItemRepository.GetNewsItemById(id);
        }

        public Envelope<NewsItemDto> GetAllNewsItems(int pageNumber, int pageSize)
        {
            Envelope<NewsItemDto> envelope =
                new Envelope<NewsItemDto>(pageNumber, pageSize, _newsItemRepository.GetAllNewsItems());
            return envelope;
        }

        public NewsItemDetailDto CreateNewsItem(NewsItemInputModel newsItem)
        {
            return _newsItemRepository.CreateNewsItem(newsItem);
        }

        public int DeleteNewsItem(int id)
        {
            return _newsItemRepository.DeleteNewsItem(id);
        }
    }
}