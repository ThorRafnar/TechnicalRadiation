using Microsoft.AspNetCore.Mvc;
using TechnicalRadiation.Models.Dtos;
using TechnicalRadiation.Models.InputModels;
using TechnicalRadiation.Services.Interfaces;
using TechnicalRadiation.WebApi.Attributes;
using TechnicalRadiation.Models.Exceptions;

namespace TechnicalRadiation.WebApi.Controllers
{
    [Route("api/categories")]
    public class CategoryController : Controller
    {
        private readonly ICategoryService _categoryService;

        public CategoryController(ICategoryService categoryService)
        {
            _categoryService = categoryService;
        }

        [HttpGet]
        [Route("")]
        public IActionResult GetAllCategories()
        {
            return Ok(_categoryService.GetAllCategories());
        }
        
        [HttpGet]
        [Route("{categoryId:int}")]
        public IActionResult GetCategoryById(int categoryId)
        {
            return Ok(_categoryService.GetCategoryById(categoryId));
        }
        
        [MyAuthorize]
        [HttpPost]
        [Route("")]
        public IActionResult CreateCategory([FromBody] CategoryInputModel cat)
        {
            if (!ModelState.IsValid) { throw new ModelFormatException(); }
            CategoryDetailDto createdCategory = _categoryService.CreateCategory(cat);
            return Created($"api/categories/{createdCategory.Id}", createdCategory);
        }

        [MyAuthorize]
        [HttpPost]
        [Route("{catId:int}/newsItems/{newsId:int}")]
        public IActionResult ConntectCategoryAndNewsItem([FromRoute] int catId, [FromRoute] int newsId)
        {
            _categoryService.ConnectCategoryAndNewsItem(catId, newsId);
            return NoContent();
        }
        
        [MyAuthorize]
        [HttpPut]
        [Route("{id:int}")]
        public IActionResult UpdateCategory([FromBody] CategoryInputModel category, int id)
        {
            if (!ModelState.IsValid) { throw new ModelFormatException(); }
            _categoryService.UpdateCategory(category, id);
            return NoContent();

        }
        
        [MyAuthorize]
        [HttpDelete]
        [Route("{id:int}")]
        public IActionResult DeleteCategory(int id)
        {
            _categoryService.DeleteCategory(id);
            return NoContent();

        }
    }
}
