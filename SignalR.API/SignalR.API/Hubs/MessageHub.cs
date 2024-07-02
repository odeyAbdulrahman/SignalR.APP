using Microsoft.AspNetCore.SignalR;

namespace SignalR.API.Hubs
{
    public class MessageHub : Hub
    {
        public MessageHub()
        {
        }

        public async Task SendMessageToAll(string title, string message, string link)
        {
            await Clients.All.SendAsync("ReceiveMessage", title, message, link);
        }

        public async Task SendMessageToUser(string userId, string title, string message, string link)
        {
            await Clients.User(userId).SendAsync("ReceiveMessage", title, message, link);
        }

        public async Task SendMessageToGroup(string groupName, string title, string message, string link)
        {
            await Clients.Group(groupName).SendAsync("ReceiveMessage", title, message, link);
        }

        public async Task AddToGroup(string groupName)
        {
            await Groups.AddToGroupAsync(Context.ConnectionId, groupName);
        }

        public async Task RemoveFromGroup(string groupName)
        {
            await Groups.RemoveFromGroupAsync(Context.ConnectionId, groupName);
        }

        public override Task OnConnectedAsync()
        {
            return base.OnConnectedAsync();
        }

        public override Task OnDisconnectedAsync(Exception? exception)
        {
            return base.OnDisconnectedAsync(exception);
        }

        protected override void Dispose(bool disposing)
        {
            base.Dispose(disposing);
        }
    }
}
