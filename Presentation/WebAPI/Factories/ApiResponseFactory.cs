using Microsoft.AspNetCore.Http;
using Todo.Services.Common.Validation;
using Todo.WebAPI.Common;

namespace Todo.WebAPI.Factories
{
    public static class ApiResponseFactory
    {
        public static ApiResponse CreateResponse(HttpContext context, ValidationResult result)
        {
            return new ApiResponse
            {
                Endpoint = context.Request.Path,
                Method = context.Request.Method,
                Message = result.Message,
                Data = result.Data,
            };
        }
    }
}