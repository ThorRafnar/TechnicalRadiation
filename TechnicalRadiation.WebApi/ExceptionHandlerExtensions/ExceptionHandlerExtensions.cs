using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Diagnostics;
using Microsoft.AspNetCore.Http; 
using System.Net;
using TechnicalRadiation.Services.Interfaces; 
using System;
using TechnicalRadiation.Models.Exceptions;
using TechnicalRadiation.Models;

namespace TechnicalRadiation.WebApi.ExceptionHandlerExtensions
{
    public static class ExceptionHandlerExtensions
    {
        public static void UseGlobalExceptionHandler(this IApplicationBuilder app)
        {
            app.UseExceptionHandler(error => 
            {
                error.Run(async context => 
                {
                    var exceptionHandlerFeature = context.Features.Get<IExceptionHandlerFeature>(); 
                    var exception = exceptionHandlerFeature.Error;
                    var statusCode = (int) HttpStatusCode.InternalServerError;

                    if (exception is ResourceNotFoundException) 
                    {
                        statusCode = (int) HttpStatusCode.NotFound;
                    } 
                    else if (exception is ModelFormatException)
                    {
                        statusCode = (int) HttpStatusCode.PreconditionFailed; 
                    }
                    else if (exception is ArgumentOutOfRangeException) 
                    {
                        statusCode = (int) HttpStatusCode.BadRequest;
                    }
                    else if (exception is UnauthorizedException)
                    {
                        statusCode = (int) HttpStatusCode.Forbidden;
                    }

                    ExceptionModel exceptionModel = new ExceptionModel 
                    {
                        StatusCode = statusCode,
                        ExceptionMessage = exception.Message,
                    };
                    context.Response.ContentType = "application/json"; 
                    context.Response.StatusCode = statusCode; 

                    await context.Response.WriteAsync(exceptionModel.ToString());
                });
            });
        }
    }
}