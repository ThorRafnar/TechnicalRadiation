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
            var cat = _dbContext.Categories.Find(id);
            var ret = _mapper.Map<CategoryDetailDto>(cat);
            AddLinksDetail(ret);
            return ret;
        }

        public CategoryDetailDto CreateCategory(CategoryInputModel cat)
        {
            //Map input model to entity
            var eCategory = _mapper.Map<Category>(cat);
            eCategory.Slug = ConvertToSlug(cat.Name);
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

        public void ConnectCategoryAndNewsItem(int catId, int newsId)
        {
            var newsItem = _dbContext.NewsItems.Include(n => n.Categories).FirstOrDefault(n => n.Id == newsId);
            var category = _dbContext.Categories.Find(catId);
            if (newsItem.Categories.Contains(category))
            {
                // If they are already connected, do nothing
                // No need to throw exception here though, in my opinion
                return;
            }
            newsItem.Categories.Add(category);
            _dbContext.SaveChanges();
            return;
        }

        public void UpdateCategory(CategoryInputModel cat, int id)
        {
            var oldCat = _dbContext.Categories.Find(id);
            oldCat.Name = cat.Name;
            oldCat.Slug = ConvertToSlug(cat.Name);
            oldCat.ModifiedBy = "TechnicalRadiationAdmin";
            oldCat.ModifiedDate = DateTime.Now;
            _dbContext.SaveChanges();
            return;
        }

        public void DeleteCategory(int id)
        {
            var cat = _dbContext.Categories.Find(id);
            _dbContext.Categories.Remove(cat);
            _dbContext.SaveChanges();
            return;
        }

        public bool DoesExist(int id)
        {
            return _dbContext.Categories.Any(c => c.Id == id);
        }

        private string ConvertToSlug(string name)
        {
            return (name.ToLower().Replace(' ', '-'));
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