using System.Net;
using System.Text.Json;
using Serilog.Context;
using FluentValidation;
using HospitalManagementSystem.DTOs.Shared;

namespace HospitalManagementSystem.Middlewares
{
    public class ExceptionMiddleware
    {
        private readonly RequestDelegate _next;
        private readonly ILogger<ExceptionMiddleware> _logger;
        private readonly IHostEnvironment _env;

        public ExceptionMiddleware(RequestDelegate next, ILogger<ExceptionMiddleware> logger, IHostEnvironment env)
        {
            _logger = logger;
            _next = next;
            _env = env;
        }

        public async Task InvokeAsync(HttpContext context)
        {
            try
            {
                await _next(context);
            }
            catch (Exception ex)
            {
                using (LogContext.PushProperty("RequestPath", context.Request.Path))
                {
                    if (ex is KeyNotFoundException || ex is ArgumentException ||
                        ex is UnauthorizedAccessException || ex is InvalidOperationException ||
                        ex is ValidationException)
                    {
                        _logger.LogWarning(ex.Message);
                    }
                    else
                    {
                        _logger.LogError(ex, "Unhandled Exception: {Message}", ex.Message);
                    }

                    await HandleExceptionAsync(context, ex);
                }
            }
        }

        private async Task HandleExceptionAsync(HttpContext context, Exception ex)
        {
            int statusCode = (int)HttpStatusCode.InternalServerError; // 500
            string message = "A server error occurred.";
            object? errorsData = null;

            switch (ex)
            {
                case KeyNotFoundException:
                    statusCode = (int)HttpStatusCode.NotFound; // 404
                    message = ex.Message;
                    break;

                case ArgumentException: 
                    statusCode = (int)HttpStatusCode.BadRequest; // 400
                    message = ex.Message;
                    break;

                case UnauthorizedAccessException:
                    statusCode = (int)HttpStatusCode.Forbidden; // 403
                    message = ex.Message;
                    break;

                case InvalidOperationException:
                    statusCode = (int)HttpStatusCode.Conflict; // 409
                    message = ex.Message;
                    break;

                case ValidationException validationEx:
                    statusCode = (int)HttpStatusCode.BadRequest; // 400
                    message = "Validation Error";
                    errorsData = validationEx.Errors.Select(e => new { e.PropertyName, e.ErrorMessage });
                    break;

                default:
                    if (_env.IsDevelopment())
                    {
                        errorsData = ex.StackTrace;
                        message = ex.Message;
                    }
                    break;
            }

            context.Response.ContentType = "application/json";
            context.Response.StatusCode = statusCode;

            var apiResponseDto = new ApiResponseDto<object>
            {
                StatusCode = statusCode,
                Data = errorsData,
                IsSuccess = false,
                Message = message,
            };

            var options = new JsonSerializerOptions { PropertyNamingPolicy = JsonNamingPolicy.CamelCase };
            var json = JsonSerializer.Serialize(apiResponseDto, options);

            await context.Response.WriteAsync(json);
        }
    }
}