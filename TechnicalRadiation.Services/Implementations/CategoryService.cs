using System;
using System.Collections.Generic;
using TechnicalRadiation.Models.Dtos;
using TechnicalRadiation.Models.Entities;
using TechnicalRadiation.Models.InputModels;
using TechnicalRadiation.Models.Exceptions;
using TechnicalRadiation.Repositories.Interfaces;
using TechnicalRadiation.Services.Interfaces;

namespace TechnicalRadiation.Services.Implementations
{
    public class CategoryService : ICategoryService
    {
        private readonly ICategoryRepository _categoryRepository;
        private readonly INewsItemRepository _newsItemRepository;

        public CategoryService(ICategoryRepository categoryRepository, INewsItemRepository newsItemRepository)
        {
            _categoryRepository = categoryRepository;
            _newsItemRepository = newsItemRepository;
        }
        
        public CategoryDetailDto GetCategoryById(int id)
        {
            if (id < 1) { throw new ArgumentOutOfRangeException("Id should not be lower than 1"); }
            if (!_categoryRepository.DoesExist(id)) { throw new ResourceNotFoundException($"Category with id {id} was not found."); }
            return _categoryRepository.GetCategoryById(id);
        }

        public List<CategoryDto> GetAllCategories()
        {
            return _categoryRepository.GetAllCategories();
        }

        public CategoryDetailDto CreateCategory(CategoryInputModel cat)
        {
            return _categoryRepository.CreateCategory(cat);
        }

        public void ConnectCategoryAndNewsItem(int catId, int newsId)
        {
            if (catId < 1 | newsId < 1) { throw new ArgumentOutOfRangeException(); }
            if (!_categoryRepository.DoesExist(catId)) { throw new ResourceNotFoundException($"Category with id {catId} was not found."); }
            if (!_newsItemRepository.DoesExist(newsId)) { throw new ResourceNotFoundException($"News item with id {newsId} was not found."); }
            _categoryRepository.ConnectCategoryAndNewsItem(catId, newsId);
            return;
        }

        public void UpdateCategory(CategoryInputModel cat, int id)
        {
            if (id < 1) { throw new ArgumentOutOfRangeException(); }
            if (!_categoryRepository.DoesExist(id)) { throw new ResourceNotFoundException($"Category with id {id} was not found."); }
            _categoryRepository.UpdateCategory(cat, id);
            return;
        }

        public void DeleteCategory(int id)
        {
            if (id < 1) { throw new ArgumentOutOfRangeException(); }
            if (!_categoryRepository.DoesExist(id)) { throw new ResourceNotFoundException($"Category with id {id} was not found, and cannot be deleted."); }
            _categoryRepository.DeleteCategory(id);
            return;
        }
    }
}