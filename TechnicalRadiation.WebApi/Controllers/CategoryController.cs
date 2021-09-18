using Microsoft.AspNetCore.Mvc;
using TechnicalRadiation.Models.Dtos;
using TechnicalRadiation.Models.InputModels;
using TechnicalRadiation.Services.Interfaces;

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
        
        //TODO Authorize
        [HttpPost]
        [Route("")]
        public IActionResult CreateCategory([FromBody] CategoryInputModel cat)
        {
            if (!ModelState.IsValid)
            {
                return StatusCode(412, cat); 
            }
            CategoryDetailDto createdCategory = _categoryService.CreateCategory(cat);
            return Created($"api/categories/{createdCategory.Id}", createdCategory);
        }

        [HttpPost]
        [Route("{catId:int}/newsItems/{newsId:int}")]
        public IActionResult ConntectCategoryAndNewsItem([FromRoute] int catId, [FromRoute] int newsId)
        {
            NewsItemDetailDto connectedNewsItem = _categoryService.ConnectCategoryAndNewsItem(catId, newsId);
            return Created($"api/{newsId}", connectedNewsItem);
        }
    }
}
