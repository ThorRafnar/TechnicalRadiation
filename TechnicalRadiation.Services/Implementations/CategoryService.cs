using System.Collections.Generic;
using TechnicalRadiation.Models.Dtos;
using TechnicalRadiation.Models.Entities;
using TechnicalRadiation.Models.InputModels;
using TechnicalRadiation.Repositories.Interfaces;
using TechnicalRadiation.Services.Interfaces;

namespace TechnicalRadiation.Services.Implementations
{
    public class CategoryService : ICategoryService
    {
        private readonly ICategoryRepository _categoryRepository;

        public CategoryService(ICategoryRepository categoryRepository)
        {
            _categoryRepository = categoryRepository;
        }
        
        public CategoryDetailDto GetCategoryById(int id)
        {
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

        public NewsItemDetailDto ConnectCategoryAndNewsItem(int catId, int newsId)
        {
            return _categoryRepository.ConnectCategoryAndNewsItem(catId, newsId);
        }
    }
}