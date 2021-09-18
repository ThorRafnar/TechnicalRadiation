using System;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using TechnicalRadiation.Models.Dtos;
using TechnicalRadiation.Models.InputModels;
using TechnicalRadiation.Services.Interfaces;

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

        //TODO Authorize
        [HttpPost]
        [Route("")]
        public IActionResult CreateNewsItem([FromBody] NewsItemInputModel newsItem)
        {
            if (!ModelState.IsValid)
            {
                return StatusCode(412, newsItem); 
            }
            NewsItemDetailDto createdNewsItem = _newsItemService.CreateNewsItem(newsItem);
            return Created($"api/{createdNewsItem.Id}", createdNewsItem);
        }
        
        //TODO returns 405 method not allowed for some reason
        [HttpDelete]
        [Route("{id:int}")]
        public IActionResult DeleteNewsItem(int id)
        {
            Console.WriteLine((id));
            return Ok(_newsItemService.DeleteNewsItem(id));
        }
        
        //TODO returns 405 method not allowed for some reason
        [HttpPut]
        [Route("api/{newsItemId}")]
        public IActionResult UpdateNewsItem([FromBody] NewsItemInputModel newsItem, int newsItemId)
        {
            if(!ModelState.IsValid){
                //lista upp villur og henda í badrequestinu
                return BadRequest("The Model is not properly formatted");
            }
            //_newsItemService.UpdateNewsItem(newsItem); 
            return NoContent();
        }
        
    }
}