//using Microsoft.AspNetCore.SignalR;

using Microsoft.AspNetCore.SignalR;

namespace TemplateService.Business.Helper.Socket
{
    public class NotifyKCCHub : Hub<ITypedHubClient>
    {

        //public async Task JoinGroup(string groupName)
        //{
        //    await Groups.AddToGroupAsync(Context.ConnectionId, groupName);
        //    //await Clients.Group(groupName).SendAsync("ReceiveMessage", $"{Context.ConnectionId} joined the group {groupName}");
        //}

        //public async Task LeaveGroup(string groupName)
        //{
        //    await Groups.RemoveFromGroupAsync(Context.ConnectionId, groupName);
        //    //await Clients.Group(groupName).SendAsync("ReceiveMessage", $"{Context.ConnectionId} left the group {groupName}");
        //}
    }
}
