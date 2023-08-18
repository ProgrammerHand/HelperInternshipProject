﻿
using Helper.Core.Exceptions;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;

namespace Helper.Infrastructure.Exceptions
{
    public sealed class ExceptionMiddleware : IMiddleware
    {
        private readonly ILogger<ExceptionMiddleware> _logger;

        public ExceptionMiddleware(ILogger<ExceptionMiddleware> logger)
        {
            _logger = logger;
        }

        public async Task InvokeAsync(HttpContext context, RequestDelegate next)
        {
            try
            {
                await next(context);
            }
            catch (Exception exception)
            {
                _logger.LogError(exception.Message);//, exception.Message);
                await HandleExceptionAsync(exception, context);
            }
        }

        private async Task HandleExceptionAsync(Exception exception, HttpContext context)
        {
            var (statusCode, error) = exception switch
            {
                CustomException => (StatusCodes.Status400BadRequest,
                    new Error(exception.GetType().Name, exception.Message)),
                _ => (StatusCodes.Status500InternalServerError, new Error("error", "There was an error."))
            };

            context.Response.StatusCode = statusCode;
            await context.Response.WriteAsJsonAsync(error);
        }

        private record Error(string Code, string Reason);
    }
}
