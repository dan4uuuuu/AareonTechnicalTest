using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using static Utils.Exceptions.CustomErrorCodes;

namespace Utils.Exceptions
{
    public class ExceptionHandlerMiddleware
    {
        private readonly RequestDelegate _next;
        private readonly ILogger logger;
        private readonly string responseContentType = "application/json";

        public ExceptionHandlerMiddleware(RequestDelegate next, ILogger<ExceptionHandlerMiddleware> logger)
        {
            this._next = next;
            this.logger = logger;
        }

        public async Task InvokeAsync(HttpContext context)
        {
            try
            {
                await this._next(context);
            }
            catch (AppException e)
            {
                context.Response.StatusCode = (int)HttpStatusCode.BadRequest;
                context.Response.ContentType = this.responseContentType;
                ErrorResponse errorResponse = new(e.Message, status: context.Response.StatusCode);
                if (e.Args.Count > 0)
                {
                    errorResponse = new(e.Message, e.Args, context.Response.StatusCode);
                }

                await context.Response.WriteAsync(errorResponse.ToJson());
            }
            catch (OperationCanceledException)
            {
            }
            catch (Exception e)
            {
                this.logger.LogError(e.Message);

                context.Response.StatusCode = (int)HttpStatusCode.InternalServerError;
                context.Response.ContentType = this.responseContentType;
                await context.Response.WriteAsync(new ErrorResponse(INTERNAL_SERVER_ERROR, status: context.Response.StatusCode).ToJson());
            }
        }
    }
}
