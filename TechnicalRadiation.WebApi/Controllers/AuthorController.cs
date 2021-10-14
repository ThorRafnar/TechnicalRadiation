using Microsoft.AspNetCore.Mvc;
using TechnicalRadiation.Models.Dtos;
using TechnicalRadiation.Models.InputModels;
using TechnicalRadiation.Services.Interfaces;
using TechnicalRadiation.WebApi.Attributes;
using TechnicalRadiation.Models.Exceptions;

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
        
        [MyAuthorize]
        [HttpPost]
        [Route("")]
        public IActionResult CreateAuthor([FromBody] AuthorInputModel authorIM)
        {
            if (!ModelState.IsValid) { throw new ModelFormatException(); }
            AuthorDetailDto createdAuthor = _authorService.CreateAuthor(authorIM);
            return Created($"api/authors/{createdAuthor.Id}", createdAuthor);
        }
        
        [MyAuthorize]
        [HttpPost]
        [Route("{authId:int}/newsItems/{newsId:int}")]
        public IActionResult ConntectAuthorAndNewsItem([FromRoute] int authId, [FromRoute] int newsId)
        {
            _authorService.ConnectAuthorAndNewsItem(authId, newsId);
            return NoContent();
        }
        
        [MyAuthorize]
        [HttpDelete]
        [Route("/{id:int}")]
        public IActionResult DeleteNewsItem(int id)
        {
            _authorService.DeleteAuthor(id);
            return NoContent();
        }
        
        [MyAuthorize]
        [HttpPut]
        [Route("{id:int}")]
        public IActionResult UpdateAuthor([FromBody] AuthorInputModel author, int id)
        {
			if (!ModelState.IsValid) { throw new ModelFormatException(); }
			_authorService.UpdateAuthor(author, id);
            return NoContent();
        }

		[MyAuthorize]
        [HttpPatch]
        [Route("{id:int}")]
        public IActionResult PartiallyUpdateAuthor([FromBody] AuthorInputModel author, int id)
        {
			_authorService.UpdateAuthor(author, id);
            return NoContent();
        }
    }
}