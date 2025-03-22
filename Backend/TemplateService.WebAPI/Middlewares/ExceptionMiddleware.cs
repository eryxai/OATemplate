#region Using ...
using Framework.Common.Exceptions.Base;
using Framework.Core.Common;
using Framework.Core.Models;
using Microsoft.AspNetCore.Http;
using System;
using System.Diagnostics;
using System.Net;
using System.Threading.Tasks;
#endregion

/*
 
 
 */
namespace TemplateService.WebAPI.Middlewares
{
    public class ExceptionMiddleware
	{
		#region Data Memners
		private readonly RequestDelegate _next;
		private readonly ILoggerService _logger;
		#endregion

		#region Constructors
		public ExceptionMiddleware(RequestDelegate next, ILoggerService logger)
		{
			_logger = logger;
			_next = next;
		} 
		#endregion

		public async Task InvokeAsync(HttpContext httpContext)
		{
			try
			{
				await _next(httpContext);
			}
			catch (Exception ex)
			{
				string errorSigniture = httpContext.Request.Path.ToString() + "\n";

				_logger.LogError($"{errorSigniture}\n Something went wrong: {ex}");
				await HandleExceptionAsync(httpContext, ex);
			}
		}

		private Task HandleExceptionAsync(HttpContext context, Exception exception)
		{
			context.Response.ContentType = "application/json";
			context.Response.StatusCode = (int)HttpStatusCode.InternalServerError;

			if (exception is BaseException be)
			{
				return context.Response.WriteAsync(new ErrorDetails()
				{
					StatusCode = context.Response.StatusCode,
					Message = "Internal Server Error from the custom middleware.",
					ErrorCode = be.ErrorCode
				}.ToString());
			}
			else
			{
				var s = new StackTrace(exception); // Gets the stack trace where the exception was thrown not where it was caught.
				var frame = s.GetFrame(0);
				var sourceMethod = frame.GetMethod();
				_logger.LogError($"Method: {sourceMethod.Name} - Class {sourceMethod.DeclaringType.FullName} : Location: {frame.GetILOffset()} : Message :{exception}");
				
				return context.Response.WriteAsync(new ErrorDetails()
				{
					StatusCode = context.Response.StatusCode,
					Message = "Internal Server Error from the custom middleware."
				}.ToString());
			}

		}

	}
}
