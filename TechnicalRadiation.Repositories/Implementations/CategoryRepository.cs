using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
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
    public class CategoryRepository : ICategoryRepository
    {
        private readonly NewsDbContext _dbContext;
        private IMapper _mapper;

        public CategoryRepository(NewsDbContext dbContext, IMapper mapper)
        {
            _dbContext = dbContext;
            _mapper = mapper;
        }

        public List<CategoryDto> GetAllCategories()
        {
            List<CategoryDto> cats = _dbContext.Categories.Select(c => new CategoryDto
            {
                Id = c.Id,
                Name = c.Name,
                Slug = c.Slug
            }).ToList();
            for (int i = 0; i < cats.Count; i++)
            {
                AddLinks(cats[i]);
            }

            return cats;
        }

        public CategoryDetailDto GetCategoryById(int id)
        {
            var cat = _dbContext.Categories.Where(c => c.Id == id).Select(n => new CategoryDetailDto
            {
                Id = n.Id,
                Name = n.Name,
                Slug = n.Slug,
                NumberOfNewsItems = n.NewsItems.Count
            }).FirstOrDefault();
            AddLinksDetail(cat);
            return cat;
        }

        public CategoryDetailDto CreateCategory(CategoryInputModel cat)
        {
            //Map input model to entity
            var eCategory = _mapper.Map<Category>(cat);
            eCategory.Slug = (cat.Name.ToLower().Replace(' ', '-'));
            eCategory.ModifiedBy = "TechnicalRadiationAdmin";
            eCategory.CreatedDate = DateTime.Now;
            eCategory.ModifiedDate = DateTime.Now;
            //Add entity to databse
            _dbContext.Categories.Add(eCategory);
            _dbContext.SaveChanges();
            //Map entity to DTO
            var category = _mapper.Map<CategoryDetailDto>(eCategory);
            AddLinksDetail(category);
            return category;
        }

        public NewsItemDetailDto ConnectCategoryAndNewsItem(int catId, int newsId)
        {
            var newsItem = _dbContext.NewsItems.Include(n=> n.Categories).FirstOrDefault(n => n.Id == newsId);
            var category = _dbContext.Categories.Find(catId);
            newsItem.Categories.Add(category);
            _dbContext.SaveChanges();
            NewsItemDetailDto newsRet = _mapper.Map<NewsItemDetailDto>(newsItem);
            return newsRet;
        }

        private void AddLinks(CategoryDto category)
        {
            category.Links.AddReference("self", new { href = $"/api/categories/{category.Id}", rel = "self", type = "GET" });
            category.Links.AddReference("edit", new { href = $"/api/categories/{category.Id}", rel = "self", type = "PUT"  });
            category.Links.AddReference("delete", new { href = $"/api/categories/{category.Id}", rel = "self", type = "DELETE" });
        }
        
        private void AddLinksDetail(CategoryDetailDto category)
        {
            category.Links.AddReference("self", new { href = $"/api/categories/{category.Id}", rel = "self", type = "GET" });
            category.Links.AddReference("edit", new { href = $"/api/categories/{category.Id}", rel = "self", type = "PUT"  });
            category.Links.AddReference("delete", new { href = $"/api/categories/{category.Id}", rel = "self", type = "DELETE" });
        }
    }
}