#region Using ...
using Microsoft.AspNetCore.Mvc;
using System;
using System.Net.Http.Headers;
using System.Threading.Tasks;
#endregion

/*
 
 
 */
namespace TemplateService.WebAPI.Auth
{
    public class UnauthorizedResult : IActionResult
    {
        public UnauthorizedResult(AuthenticationHeaderValue authHeaderValue, IActionResult innerResult)
        {
            AuthHeaderValue = authHeaderValue;
            InnerResult = innerResult;
        }
        public AuthenticationHeaderValue AuthHeaderValue { get; }
        public IActionResult InnerResult { get; }
   
        //public async Task<HttpResponseMessage> ExecuteAsync(ActionContext actionContext)
        //{
        //    HttpResponseMessage response = await InnerResult.ExecuteResultAsync(cancellationToken);

        //    if (response.StatusCode == HttpStatusCode.Unauthorized)
        //    {
        //        // Only add one challenge per authentication scheme.
        //        if (response.Headers.WwwAuthenticate.All(h => h.Scheme != AuthHeaderValue.Scheme))
        //        {
        //            response.Headers.WwwAuthenticate.Add(AuthHeaderValue);
        //        }
        //    }

        //    return response;
        //}

        public Task ExecuteResultAsync(ActionContext context)
        {
            throw new NotImplementedException();
        }
    }
}
