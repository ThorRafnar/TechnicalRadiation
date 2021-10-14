using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using AutoMapper;
using Microsoft.EntityFrameworkCore;
using TechnicalRadiation.Models;
using TechnicalRadiation.Models.Dtos;
using TechnicalRadiation.Models.Entities;
using TechnicalRadiation.Models.Extensions;
using TechnicalRadiation.Models.InputModels;
using TechnicalRadiation.Repositories.Data;
using TechnicalRadiation.Repositories.Interfaces;

namespace TechnicalRadiation.Repositories.Implementations
{
    public class AuthorRepository : IAuthorRepository
    {
        private readonly NewsDbContext _dbContext;
        private readonly INewsItemRepository _newsItemRepository;
        private IMapper _mapper;

        public AuthorRepository(NewsDbContext dbContext, IMapper mapper, INewsItemRepository newsItemRepository)
        {
            _dbContext = dbContext;
            _mapper = mapper;
            _newsItemRepository = newsItemRepository;
        }
        
        public List<AuthorDto> GetAllAuthors()
        {
            var authors = _dbContext.Authors.Select(a => new AuthorDto
            {
                Id = a.Id,
                Name = a.Name
            }).ToList();
            foreach (AuthorDto author in authors)
            {
                AddLinks(author);
            }
            return authors;
        }

        public AuthorDetailDto GetAuthorById(int id)
        {
            var author = _dbContext.Authors.Find(id);
            if (author == null) { return new AuthorDetailDto(); }
            var ret = _mapper.Map<AuthorDetailDto>(author);
            AddLinksDetail(ret);
            return ret;
        }

        public List<NewsItemDto> GetNewsItemsByAuthorId(int authorId)
        {
            var newsList = _dbContext.NewsItems.Where(NewsItem => NewsItem.Authors.Any(a => a.Id == authorId))
                .Select(n => new NewsItemDto
                {
                    Id = n.Id,
                    Title = n.Title,
                    ImgSource = n.ImgSource,
                    ShortDescription = n.ShortDescription
                }).ToList();
            foreach (NewsItemDto news in newsList)
            {
                _newsItemRepository.AddLinks(news);
            }

            return newsList;
        }

        public AuthorDetailDto CreateAuthor(AuthorInputModel authorInputModel)
        {
            // Map Input model to Entity
            var author = _mapper.Map<Author>(authorInputModel);
            //Add entity to databse
            author.ModifiedBy = "TechnicalRadiationAdmin";
            author.ModifiedDate = DateTime.Now;
            author.CreatedDate = DateTime.Now;
            _dbContext.Authors.Add(author);
            _dbContext.SaveChanges();
            //Map entity to DTO
            var retAuthor = _mapper.Map<AuthorDetailDto>(author);
            AddLinksDetail(retAuthor);
            return retAuthor;
        }

        public void ConnectAuthorAndNewsItem(int authId, int newsId)
        {
            var newsItem = _dbContext.NewsItems.Include(n=> n.Authors).FirstOrDefault(n => n.Id == newsId);
            var author = _dbContext.Authors.Find(authId);
            newsItem.Authors.Add(author);
            _dbContext.SaveChanges();
            return;
        }

        public void DeleteAuthor(int id)
        {
            var author = _dbContext.Authors.Find(id);
            _dbContext.Authors.Remove(author);
            _dbContext.SaveChanges();
            return;
        }

        public void UpdateAuthor(AuthorInputModel author, int id)
        {
            var oldAuthor = _dbContext.Authors.Find(id);
            oldAuthor.Name = author.Name;
            oldAuthor.ProfileImgSource = author.ProfileImgSource;
            oldAuthor.Bio = author.Bio;
            oldAuthor.ModifiedBy = "TechnicalRadiationAdmin";
            oldAuthor.ModifiedDate = DateTime.Now;
            _dbContext.SaveChanges();
            return;
        }

        public void PartiallyUpdateAuthor(AuthorInputModel author, int id)
        {
            var oldAuthor = _dbContext.Authors.Find(id);
            if (!String.IsNullOrEmpty(author.Name)) { oldAuthor.Name = author.Name; }
            if (!String.IsNullOrEmpty(author.ProfileImgSource)) { oldAuthor.ProfileImgSource = author.ProfileImgSource; }
            if (!String.IsNullOrEmpty(author.Bio)) { oldAuthor.Bio = author.Bio; }
            oldAuthor.ModifiedBy = "TechnicalRadiationAdmin";
            oldAuthor.ModifiedDate = DateTime.Now;
            _dbContext.SaveChanges();
            return;
        }
        
        public bool DoesExist(int id)
        {
            return _dbContext.Authors.Any(a => a.Id == id);
        }

        //I know these do the same thing, but whatever
        private void AddLinks(AuthorDto author)
        {
            //Get Ids of news item by author
            List<int> newsItemIds = _dbContext.NewsItems.Where(newsItem => newsItem.Authors.Any(a => a.Id == author.Id))
                .Select(x=>x.Id).ToList();
            
            //Generate links for those news items to add for HATEOAS
            List<IDictionary<string, string>> newsDetailLinks = new List<IDictionary<string, string>>(new List<Dictionary<string, string>>());
            for (int i = 0; i < newsItemIds.Count; i++)
            {
                string address = $"/api/{newsItemIds[i]}";
                Dictionary<string, string> item = new Dictionary<string, string>();
                item.Add("href", address);
                item.Add("rel", "newsItem");
                item.Add("type", "GET");
                newsDetailLinks.Add(item);
            }

            author.Links.AddReference("self", new { href = $"/api/authors/{author.Id}", rel = "self", type = "GET" });
            author.Links.AddReference("edit", new { href = $"/api/authors/{author.Id}", rel = "self", type = "PUT"  });
            author.Links.AddReference("delete", new { href = $"/api/authors/{author.Id}", rel = "self", type = "DELETE" });
            author.Links.AddReference("newsItems", new { href = $"/api/authors/{author.Id}/newsitems", rel = "newsItems", type = "GET"  });
            author.Links.AddListReference("newsItemsDetailed", newsDetailLinks);
        }
        
        //I know these two do the same thing, but whatever
        private void AddLinksDetail(AuthorDetailDto author)
        {
            //Get Ids of news item by author
            List<int> newsItemIds = _dbContext.NewsItems.Where(newsItem => newsItem.Authors.Any(a => a.Id == author.Id))
                .Select(x=>x.Id).ToList();
            
            //Generate links for those news items to add for HATEOAS
            List<IDictionary<string, string>> newsDetailLinks = new List<IDictionary<string, string>>(new List<Dictionary<string, string>>());
            for (int i = 0; i < newsItemIds.Count; i++)
            {
                string address = $"/api/{newsItemIds[i]}";
                Dictionary<string, string> item = new Dictionary<string, string>();
                item.Add("href", address);
                item.Add("rel", "newsItem");
                item.Add("type", "GET");
                newsDetailLinks.Add(item);
            }

            author.Links.AddReference("self", new { href = $"/api/authors/{author.Id}", rel = "self", type = "GET" });
            author.Links.AddReference("edit", new { href = $"/api/authors/{author.Id}", rel = "self", type = "PUT"  });
            author.Links.AddReference("delete", new { href = $"/api/authors/{author.Id}", rel = "self", type = "DELETE" });
            author.Links.AddReference("newsItems", new { href = $"/api/authors/{author.Id}/newsitems", rel = "newsItems", type = "GET"  });
            author.Links.AddListReference("newsItemsDetailed", newsDetailLinks);
        }
    }
}