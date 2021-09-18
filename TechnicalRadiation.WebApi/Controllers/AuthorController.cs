using Microsoft.AspNetCore.Mvc;
using TechnicalRadiation.Models.Dtos;
using TechnicalRadiation.Models.InputModels;
using TechnicalRadiation.Services.Interfaces;

namespace TechnicalRadiation.WebApi.Controllers
{
    [Route("api/authors")]
    public class AuthorController : Controller
    {
        private readonly IAuthorService _authorService;

        public AuthorController(IAuthorService authorService)
        {
            _authorService = authorService;
        }

        [HttpGet]
        [Route("")]
        public IActionResult GetAllCategories()
        {
            return Ok(_authorService.GetAllAuthors());
        }
        
        [HttpGet]
        [Route("{authorId:int}")]
        public IActionResult GetAuthorById(int authorId)
        {
            return Ok(_authorService.GetAuthorById(authorId));
        }
        
        [HttpGet]
        [Route("{authorId:int}/newsitems")]
        public IActionResult GetNewsItemsByAuthorId(int authorId)
        {
            return Ok(_authorService.GetNewsItemsByAuthorId(authorId));
        }
        
        //TODO Authorize
        [HttpPost]
        [Route("")]
        public IActionResult CreateAuthor([FromBody] AuthorInputModel authorIM)
        {
            if (!ModelState.IsValid)
            {
                return StatusCode(412, authorIM); 
            }
            AuthorDetailDto createdAuthor = _authorService.CreateAuthor(authorIM);
            return Created($"api/authors/{createdAuthor.Id}", createdAuthor);
        }
        
        [HttpPost]
        [Route("{authId:int}/newsItems/{newsId:int}")]
        public IActionResult ConntectAuthorAndNewsItem([FromRoute] int authId, [FromRoute] int newsId)
        {
            NewsItemDetailDto connectedNewsItem = _authorService.ConnectAuthorAndNewsItem(authId, newsId);
            return Created($"api/{newsId}", connectedNewsItem);
        }
        
        //TODO returns 405 method not allowed for some reason
        [HttpDelete]
        [Route("/{id:int}")]
        public IActionResult DeleteNewsItem([FromRoute] int id)
        {
            return Ok();
        }
        
        //TODO returns 405 method not allowed for some reason
        [HttpPut]
        [Route("/{id:int}")]
        public IActionResult UpdateNewsItem([FromBody] NewsItemInputModel news, int id)
        {
            return Ok();
        }
    }
}