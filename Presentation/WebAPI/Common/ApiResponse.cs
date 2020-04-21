using System;
using System.Collections.Generic;
using Microsoft.AspNetCore.Http;
using Newtonsoft.Json;
using Todo.Services.Common.Validation;

namespace Todo.WebAPI.Common
{
    public class ApiResponse
    {
        public ApiResponse(HttpContext context, ValidationResult result)
        {
            Endpoint = context.Request.Path;
            Method = context.Request.Method;
            Message = result.Message;
            Data = result.Data;
        }

        public ApiResponse(HttpContext context, Exception exception)
        {
            Endpoint = context.Request.Path;
            Method = context.Request.Method;
            Message = exception.Message;
        }

        public string Endpoint { get; }

        public string Method { get; }

        public string Message { get; }

        public IDictionary<string, object> Data { get; }

        public override string ToString()
        {
            return JsonConvert.SerializeObject(this);
        }
    }
}