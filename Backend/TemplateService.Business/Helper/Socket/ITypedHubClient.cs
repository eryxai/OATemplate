using TemplateService.Core.Models.ViewModels.NotifyReturn;
using System.Threading.Tasks;

namespace TemplateService.Business.Helper.Socket
{
    public interface ITypedHubClient
    {
        Task BroadcastMessage(NotifyReturnViewModel entity);
    }
}
