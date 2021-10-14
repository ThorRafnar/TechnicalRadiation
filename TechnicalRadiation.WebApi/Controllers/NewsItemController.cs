using System;
using Microsoft.AspNetCore.Mvc;
using TechnicalRadiation.Models.Dtos;
using TechnicalRadiation.Models.InputModels;
using TechnicalRadiation.Services.Interfaces;
using TechnicalRadiation.WebApi.Attributes;
using TechnicalRadiation.Models.Exceptions;


namespace TechnicalRadiation.WebApi.Controllers
{
    [Route("api")]
    public class NewsItemController : Controller
    {
        private readonly INewsItemService _newsItemService;

        public NewsItemController(INewsItemService newsItemService)
        {
            _newsItemService = newsItemService;
        }

        [HttpGet]
        [Route("")]
        public IActionResult GetAllNewsItems([FromQuery] int pageNumber=1, [FromQuery] int pageSize=25)
        {
            return Ok(_newsItemService.GetAllNewsItems(pageNumber, pageSize));
        }
        
        [HttpGet]
        [Route("{id:int}")]
        public IActionResult GetNewsItemsById(int id)
        {
            return Ok(_newsItemService.GetNewsItemById(id));
        }

        [MyAuthorize]
        [HttpPost]
        [Route("")]
        public IActionResult CreateNewsItem([FromBody] NewsItemInputModel newsItem)
        {
            if(!ModelState.IsValid) { throw new ModelFormatException(); }
            NewsItemDetailDto createdNewsItem = _newsItemService.CreateNewsItem(newsItem);
            return Created($"api/{createdNewsItem.Id}", createdNewsItem);
        }
        
        [MyAuthorize]
        [HttpDelete]
        [Route("{id:int}")]
        public IActionResult DeleteNewsItem(int id)
        {
            _newsItemService.DeleteNewsItem(id);
            return NoContent();
        }
        
        [MyAuthorize]
        [HttpPut]
        [Route("{id:int}")]
        public IActionResult UpdateNewsItem([FromBody] NewsItemInputModel newsItem, int id)
        {
            if(!ModelState.IsValid) { throw new ModelFormatException(); }
            _newsItemService.UpdateNewsItem(newsItem, id);
            return NoContent();
        }
        
        [MyAuthorize]
        [HttpPatch]
        [Route("{id:int}")]
        public IActionResult PartiallyUpdateNewsItem([FromBody] NewsItemInputModel newsItem, int id)
        {
            _newsItemService.PartiallyUpdateNewsItem(newsItem, id);
            return NoContent();
        }
    }
}