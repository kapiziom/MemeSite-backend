using MemeSite.Domain.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;
using System;
using System.Net;
using System.Threading.Tasks;

namespace MemeSite.Api.Middleware
{
    public class ExceptionsHandlingMiddleware
    {
        private readonly RequestDelegate _next;

        public ExceptionsHandlingMiddleware(RequestDelegate next)
        {
            _next = next;
        }

        public async Task InvokeAsync(HttpContext httpContext)
        {
            try
            {
                await _next(httpContext);
            }
            catch (MemeSiteException ex)
            {
                await HandleHttpExceptionAsync(httpContext, ex);
            }
            catch (Exception ex)
            {
                await HandleUnhandledExceptionAsync(httpContext, ex);
            }
        }

        private async Task HandleHttpExceptionAsync(HttpContext context, MemeSiteException exception)
        {

            if (!context.Response.HasStarted)
            {
                int statusCode = exception.StatusCode;
                string message = exception.Message;
                object result = exception.Result;

                var response = new object();
                if (result != null)
                {
                    response = new { statusCode, result };
                }
                else response = new { statusCode, message };

                var jsonRes = JsonConvert.SerializeObject(response);

                context.Response.Clear();

                context.Response.ContentType = "application/json";
                context.Response.StatusCode = statusCode;

                await context.Response.WriteAsync(jsonRes);
            }
        }


        private async Task HandleUnhandledExceptionAsync(HttpContext context,
                                Exception exception)
        {

            if (!context.Response.HasStarted)
            {
                int statusCode = (int)HttpStatusCode.InternalServerError; //StatusCode = 500
                string message = string.Empty;
#if DEBUG
                message = exception.Message;
#else
                message = "An unhandled exception has occurred";
#endif
                context.Response.Clear();
                context.Response.ContentType = "application/json";
                context.Response.StatusCode = statusCode;

                var result = new ExceptionMessage(message).ToString();
                await context.Response.WriteAsync(result);
            }
        }
    }
}
