#region Using ...
using System;
using Framework.Common.Exceptions;
using Framework.Core.Common;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
#endregion

/*
 
 
 */
namespace TemplateService.WebAPI.Filters
{
    public class HttpResponseExceptionFilter : IActionFilter, IOrderedFilter
	{
		#region Data Members
		IServiceProvider _serviceProvider;
		ILoggerService _logger;
		#endregion

		#region Constructors
		public HttpResponseExceptionFilter()
		{

		} 
		#endregion

		public int Order { get; } = int.MaxValue - 10;

		public void OnActionExecuting(ActionExecutingContext context) { }

		public void OnActionExecuted(ActionExecutedContext context)
		{
			if (context.Exception != null)
			{
				_serviceProvider = context.HttpContext.RequestServices;
				_logger = _serviceProvider.GetService(typeof(ILoggerService)) as ILoggerService;

				_logger.LogError(context.Exception);

				if (context.Exception is HttpResponseException exception)
				{
					context.Result = new ObjectResult(exception.Value)
					{
						StatusCode = exception.Status,
					};
					context.ExceptionHandled = true;
				}
			}
		}
	}
}
