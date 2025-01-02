using API.Context;
using API.Response;
using Microsoft.AspNetCore.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using System;
using System.Net;
using System.Text.Json;

namespace API.Exception
{
    public class GlobalExceptionHandler : IExceptionHandler
    {
        private readonly IConfiguration _configuration;
        private readonly IHttpContextAccessor _httpContextAccessor;
        private readonly ILogger<GlobalExceptionHandler> _logger;

        public GlobalExceptionHandler(IConfiguration configuration, IHttpContextAccessor httpContextAccessor, ILogger<GlobalExceptionHandler> logger)
        {
            _configuration = configuration;
            _httpContextAccessor = httpContextAccessor;
            _logger = logger;
        }

        public async ValueTask<bool> TryHandleAsync(HttpContext httpContext, System.Exception exception, CancellationToken cancellationToken)
        {
            if (exception == null)
            {
                return false;
            }
            _logger.LogError(exception, "An error occurred while processing your request");
            var result = _configuration.GetValue<bool>("IsDevelopment")
                             ? exception.ToString()
                             : "An unexpected error occurred. Please try again later.";

            var errorResponse = new ErrorResponse
            {
                Message = "An unexpected error occurred.",
                TraceIdentifier = httpContext.TraceIdentifier,
                Detailed = result
            };
            httpContext.Response.StatusCode = (int)HttpStatusCode.InternalServerError;
            httpContext.Response.ContentType = "application/json";
            var jsonResponse = JsonSerializer.Serialize(errorResponse);
            await httpContext.Response.WriteAsync(jsonResponse, cancellationToken);
            return true;
        }
    }
}


