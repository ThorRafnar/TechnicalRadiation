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

        void ConnectCategoryAndNewsItem(int catId, int newsId);
        void UpdateCategory(CategoryInputModel cat, int id);
        void DeleteCategory(int id);
        bool DoesExist(int id);
    }
}