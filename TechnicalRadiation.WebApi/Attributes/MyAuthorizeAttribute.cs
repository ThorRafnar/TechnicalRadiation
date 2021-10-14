using Microsoft.AspNetCore.Mvc.Filters;
using TechnicalRadiation.Models.Exceptions;
using System;

namespace TechnicalRadiation.WebApi.Attributes
{
    public class MyAuthorizeAttribute : ActionFilterAttribute
    {
        public override void OnActionExecuting(ActionExecutingContext context)
        {
            string key = context.HttpContext.Request.Headers["Authorization"];
            //My glorious hardcoded key
            if (key != "Secret")
            {
                throw new UnauthorizedException(); 
            }
        }
    }
}