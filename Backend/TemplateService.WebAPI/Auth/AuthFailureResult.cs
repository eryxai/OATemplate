#region Using ...
using Framework.Common.Enums;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Threading;
using System.Threading.Tasks;
#endregion

/*
 
 
 */
namespace TemplateService.WebAPI.Auth
{
    public class AuthFailureResult : IActionResult
    {
        public AuthFailureResult(string reasonPhrase, HttpResponse response)
        {
            ReasonPhrase = reasonPhrase;
            //Response = response;
        }
        public string ReasonPhrase { get; }
        public HttpResponse Request { get; }

        public Task<HttpResponse> ExecuteAsync(CancellationToken cancellationToken)
        {
            return Task.FromResult(Execute());
        }

        public Task ExecuteResultAsync(ActionContext context)
        {
            return Task.FromResult(Execute());
        }

        private HttpResponse Execute()
        {
            Request.StatusCode = ApplicationConstants.AuthFailureCode;
            //TextWriter textWriter = Request.Body as TextWriter;


            return Request;
        }

    }
}
