#region Using ...
using TemplateService.WebAPI.Models;
using Framework.Common.Exceptions.Base;
using Framework.Core.Common;
using Framework.Core.Models;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Diagnostics;
using Microsoft.AspNetCore.Http;
using System;
using System.IO;
using System.Net;
using System.Text.Json;
#endregion

/*
 
 
 */
namespace TemplateService.WebAPI.Middlewares
{
    public static class ExceptionMiddlewareExtensions
    {
        public static void ConfigureExceptionHandler(this IApplicationBuilder app, ILoggerService logger)
        {
            app.UseExceptionHandler(appError =>
            {
                appError.Run(async context =>
                {
                    context.Response.StatusCode = (int)HttpStatusCode.InternalServerError;
                    context.Response.ContentType = "application/json";
                    var contextFeature = context.Features.Get<IExceptionHandlerFeature>();
                    if (contextFeature != null)
                    {
                        string errorSigniture = context.Request.Path.ToString() + "\n";
                        ExceptionModel exceptionModel = new ExceptionModel
                        {
                            RequestTime = DateTime.Now,
                            ErrorCode = context.Response.StatusCode,
                            StackTrace = contextFeature.Error.StackTrace,
                            StatusCode = context.Response.StatusCode,
                            Message = errorSigniture + "Internal Server Error."
                        };

                        //  logger.LogError($"Something went wrong: {contextFeature.Error}");
                        if (contextFeature.Error is BaseException be)
                        {
                            if (be.ErrorCode != 0)
                            {
                                context.Response.StatusCode = be.ErrorCode;
                                exceptionModel.StatusCode = be.ErrorCode;
                            }
                            await context.Response.WriteAsJsonAsync(be.Message);
                        }
                        else
                        {
                            await context.Response.WriteAsync(new ErrorDetails()
                            {
                                StatusCode = context.Response.StatusCode,
                                Message = "Internal Server Error."
                            }.ToString());
                        }
                        string jsonString = JsonSerializer.Serialize(exceptionModel);
                        logger.LogError(jsonString);

                    }
                });
            });
        }

        public static void ConfigureCustomExceptionMiddleware(this IApplicationBuilder app)
        {
            app.UseMiddleware<ExceptionMiddleware>();
        }
    }
}
