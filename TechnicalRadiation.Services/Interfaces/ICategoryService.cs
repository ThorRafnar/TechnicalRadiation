using System.Collections.Generic;
using TechnicalRadiation.Models.Dtos;
using TechnicalRadiation.Models.Entities;
using TechnicalRadiation.Models.InputModels;

namespace TechnicalRadiation.Services.Interfaces
{
    public interface ICategoryService
    {
        CategoryDetailDto GetCategoryById(int id);
        List<CategoryDto> GetAllCategories();

        CategoryDetailDto CreateCategory(CategoryInputModel cat);
        NewsItemDetailDto ConnectCategoryAndNewsItem(int catId, int newsId);
    }
}