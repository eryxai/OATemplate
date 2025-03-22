using Microsoft.AspNetCore.Http;

namespace TemplateService.Core.Models.ViewModels.NotifyReturn
{
    public class NotifyReturnViewModel
    {
        private readonly IHttpContextAccessor _httpContext;

        public NotifyReturnViewModel(IHttpContextAccessor httpContext = null)
        {
            if (httpContext != null)
            {
                this._httpContext = httpContext;

                if (this._httpContext != null && this._httpContext.HttpContext != null)
                {
                    Microsoft.Extensions.Primitives.StringValues authorization;

                    if (this._httpContext.HttpContext.Request.Headers.TryGetValue("Authorization", out authorization))
                    {
                        var token = authorization.ToString().Split(" ")[1];
                        if (token != null)
                            IdentityCurrentUser = token;

                    }
                }
            }
        }
        public NotifyEntityTypeEnum NotifyEntityType { get; set; }
        public string WrongTitle { get; set; }
        public string WrongMessage { get; set; }
        public long userId { get; set; }
        public string IdentityCurrentUser { get; set; }
        public string DataEn { get; set; }
        public string DataAr { get; set; }

    }

    public enum NotifyEntityTypeEnum
    {
        Wrong = 1,
        Transactions,
        Notification,
        ReloadMenu ,
        CameraTransactions

    }

}
