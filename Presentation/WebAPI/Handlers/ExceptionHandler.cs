using System;
using System.Net;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Diagnostics;
using Microsoft.AspNetCore.Http;
using Todo.Domain.Exceptions;
using Todo.Services.Common.Exceptions;
using Todo.WebAPI.Common;

namespace Todo.WebAPI.Handlers
{
    public static class ExceptionHandler
    {
        public static async Task Handle(HttpContext context)
        {
            var feature = context.Features.Get<IExceptionHandlerPathFeature>();
            var exception = feature.Error;

            var response = new ApiErrorResponse
            {
                Endpoint = context.Request.Path,
                Method = context.Request.Method,
                Error = exception.Message
            };

            context.Response.ContentType = "application/json";
            context.Response.StatusCode = (int)GetStatusCode(exception);
            await context.Response.WriteAsync(response.ToString());
        }

        private static HttpStatusCode GetStatusCode(Exception exception)
        {
            switch (exception)
            {
                case BadRequestException _ when exception is BadRequestException:
                case ItemAlreadyStartedException _ when exception is ItemAlreadyStartedException:
                case ItemPreviouslyCancelledException _ when exception is ItemPreviouslyCancelledException:
                case ItemPreviouslyCompletedException _ when exception is ItemPreviouslyCompletedException:
                case ValidationException _ when exception is ValidationException:
                    return HttpStatusCode.BadRequest;

                case NotFoundException _ when exception is NotFoundException:
                    return HttpStatusCode.NotFound;

                default:
                    return HttpStatusCode.InternalServerError;
            }
        }
    }
}