using TemplateService.Core.Models.ViewModels.NotifyReturn;
using System.Threading.Tasks;

namespace TemplateService.Business.Helper.SignalR
{
    public interface ITypedHubClient
    {
        //  Task BroadcastMessage(NotifyReturnViewModel entity);

        Task BroadcastMessage(NotifyReturnViewModel entity);
        Task DatasetReview(NotifyReturnViewModel entity);
        Task NEBTReview(NotifyReturnViewModel entity);
        Task ChartReview(NotifyReturnViewModel entity);
    }
}
