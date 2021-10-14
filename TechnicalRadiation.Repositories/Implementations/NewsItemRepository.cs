using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography.X509Certificates;
using AutoMapper;
using Microsoft.EntityFrameworkCore;
using TechnicalRadiation.Models.Dtos;
using TechnicalRadiation.Models.Entities;
using TechnicalRadiation.Models.Extensions;
using TechnicalRadiation.Models.InputModels;
using TechnicalRadiation.Repositories.Data;
using TechnicalRadiation.Repositories.Interfaces;

namespace TechnicalRadiation.Repositories.Implementations
{
    public class NewsItemRepository : INewsItemRepository
    {

        private readonly NewsDbContext _dbContext;
        private IMapper _mapper;

        public NewsItemRepository(NewsDbContext dbContext, IMapper mapper)
        {
            _dbContext = dbContext;
            _mapper = mapper;
        }

        public List<NewsItemDto> GetAllNewsItems()
        {
            List<NewsItemDto> newsList = _dbContext.NewsItems.OrderByDescending(n => n.PublishDate).Select(n => new NewsItemDto
            {
                Id = n.Id,
                Title = n.Title,
                ImgSource = n.ImgSource,
                ShortDescription = n.ShortDescription
            }).ToList();
            for (int i = 0; i < newsList.Count; i++)
            {
                AddLinks(newsList[i]);
            }
            return newsList;
        }

        public NewsItemDetailDto GetNewsItemById(int id)
        {
            var news = _dbContext.NewsItems.Find(id);
            var retNews = _mapper.Map<NewsItemDetailDto>(news);
            AddLinksDetail(retNews);
            return retNews;
        }

        public NewsItemDetailDto CreateNewsItem(NewsItemInputModel newsItem)
        {
            var news = _mapper.Map<NewsItem>(newsItem);
            _dbContext.NewsItems.Add(news);
            _dbContext.SaveChanges();
            var newsReturn = _mapper.Map<NewsItemDetailDto>(news);
            AddLinksDetail(newsReturn);
            return newsReturn;
        }

        public void DeleteNewsItem(int id)
        {
            var news = _dbContext.NewsItems.Find(id);
            _dbContext.NewsItems.Remove(news);
            _dbContext.SaveChanges();
            return;

        }

        public void UpdateNewsItem(NewsItemInputModel news, int id)
        {
            var oldNews = _dbContext.NewsItems.Find(id);
            oldNews.Title = news.Title;
            oldNews.ImgSource = news.ImgSource;
            oldNews.ShortDescription = news.ShortDescription;
            oldNews.LongDescription = news.LongDescription;
            oldNews.PublishDate = news.PublishDate;
            oldNews.ModifiedBy = "TechnicalRadiationAdmin";
            oldNews.ModifiedDate = DateTime.Now;
            _dbContext.SaveChanges();
            return;
        }
        
        public void PartiallyUpdateNewsItem(NewsItemInputModel news, int id)
        {
            var oldNews = _dbContext.NewsItems.Find(id);
            if (!String.IsNullOrEmpty(news.Title)) { oldNews.Title = news.Title; }
            if (!String.IsNullOrEmpty(news.ImgSource)) { oldNews.ImgSource = news.ImgSource; }
            if (!String.IsNullOrEmpty(news.ShortDescription)) { oldNews.ShortDescription = news.ShortDescription; }
            if (!String.IsNullOrEmpty(news.LongDescription)) { oldNews.LongDescription = news.LongDescription; }
            if (news.PublishDate != DateTime.MinValue) { oldNews.PublishDate = news.PublishDate; }
            oldNews.ModifiedBy = "TechnicalRadiationAdmin";
            oldNews.ModifiedDate = DateTime.Now;
            _dbContext.SaveChanges();
            return;
        }

        public bool DoesExist(int id)
        {
            return _dbContext.NewsItems.Any(n => n.Id == id);
        }
        
        //I want this public to avoid excessive duplication
        public void AddLinks(NewsItemDto news)
        {
            //Get Ids of authors who wrote the news item
            List<int> authorIds = _dbContext.Authors.Where(author => author.NewsItems.Any(n => n.Id == news.Id))
                .Select(x=>x.Id).ToList();
            
            //Generate links for those news items to add for HATEOAS
            List<IDictionary<string, string>> authorLinks = new List<IDictionary<string, string>>(new List<Dictionary<string, string>>());
            for (int i = 0; i < authorIds.Count; i++)
            {
                string address = $"/api/authors/{authorIds[i]}";
                Dictionary<string, string> item = new Dictionary<string, string>();
                item.Add("href", address);
                item.Add("rel", "author");
                item.Add("type", "GET");
                authorLinks.Add(item);
            }
            
            //Get Ids of categories of the news item
            List<int> catIds = _dbContext.Categories.Where(cat => cat.NewsItems.Any(n => n.Id == news.Id))
                .Select(x=>x.Id).ToList();
            
            //Generate links for those categories to add for HATEOAS
            List<IDictionary<string, string>> categoryLinks = new List<IDictionary<string, string>>(new List<Dictionary<string, string>>());
            for (int i = 0; i < catIds.Count; i++)
            {
                string address = $"/api/categories/{catIds[i]}";
                Dictionary<string, string> item = new Dictionary<string, string>();
                item.Add("href", address);
                item.Add("rel", "category");
                item.Add("type", "GET");
                categoryLinks.Add(item);
            }

            news.Links.AddReference("self", new { href = $"/api/{news.Id}", rel = "self", type = "GET" });
            news.Links.AddReference("edit", new { href = $"/api/{news.Id}", rel = "self", type = "PUT"  });
            news.Links.AddReference("delete", new { href = $"/api/{news.Id}", rel = "self", type = "DELETE" });
            news.Links.AddListReference("authors", authorLinks);
            news.Links.AddListReference("categories", categoryLinks);
        }
        
        
        private void AddLinksDetail(NewsItemDetailDto news)
        {
            //Get Ids of authors who wrote the news item
            List<int> authorIds = _dbContext.Authors.Where(author => author.NewsItems.Any(n => n.Id == news.Id))
                .Select(x=>x.Id).ToList();
            
            //Generate links for those news items to add for HATEOAS
            List<IDictionary<string, string>> authorLinks = new List<IDictionary<string, string>>(new List<Dictionary<string, string>>());
            for (int i = 0; i < authorIds.Count; i++)
            {
                string address = $"/api/authors/{authorIds[i]}";
                Dictionary<string, string> item = new Dictionary<string, string>();
                item.Add("href", address);
                item.Add("rel", "author");
                item.Add("type", "GET");
                authorLinks.Add(item);
            }
            
            //Get Ids of categories of the news item
            List<int> catIds = _dbContext.Categories.Where(cat => cat.NewsItems.Any(n => n.Id == news.Id))
                .Select(x=>x.Id).ToList();
            
            //Generate links for those categories to add for HATEOAS
            List<IDictionary<string, string>> categoryLinks = new List<IDictionary<string, string>>(new List<Dictionary<string, string>>());
            for (int i = 0; i < catIds.Count; i++)
            {
                string address = $"/api/categories/{catIds[i]}";
                Dictionary<string, string> item = new Dictionary<string, string>();
                item.Add("href", address);
                item.Add("rel", "category");
                item.Add("type", "GET");
                categoryLinks.Add(item);
            }

            news.Links.AddReference("self", new { href = $"/api/{news.Id}", rel = "self", type = "GET" });
            news.Links.AddReference("edit", new { href = $"/api/{news.Id}", rel = "self", type = "PUT"  });
            news.Links.AddReference("delete", new { href = $"/api/{news.Id}", rel = "self", type = "DELETE" });
            news.Links.AddListReference("authors", authorLinks);
            news.Links.AddListReference("categories", categoryLinks);
        }
    }
}