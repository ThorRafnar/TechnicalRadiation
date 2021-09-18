using System.Collections.Generic;
using TechnicalRadiation.Models.Dtos;
using TechnicalRadiation.Models.InputModels;

namespace TechnicalRadiation.Repositories.Interfaces
{
    public interface ICategoryRepository
    {
        List<CategoryDto> GetAllCategories();

        CategoryDetailDto GetCategoryById(int id);

        CategoryDetailDto CreateCategory(CategoryInputModel cat);

        NewsItemDetailDto ConnectCategoryAndNewsItem(int catId, int newsId);
    }
}