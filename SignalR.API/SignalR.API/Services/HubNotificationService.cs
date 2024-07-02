using SignalR.API.Dto;
using SignalR.API.Extensions;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.SignalR.Client;
using Microsoft.IdentityModel.Protocols.OpenIdConnect;

namespace SignalR.API.Services;
public class HubNotificationService : IHubNotificationService, IDisposable
{
    private readonly HubConnection Connection;
    public HubNotificationService(string hubUrl)
    {
        Connection = new HubConnectionBuilder().WithUrl(hubUrl, options =>
        {
            options.AccessTokenProvider = async () =>
            await HttpContextAccessorUtility.Current().GetTokenAsync(OpenIdConnectParameterNames.AccessToken);
        }).Build();
    }

    public async Task StartAsync()
    {
        if (Connection.State == HubConnectionState.Disconnected)
            await Connection.StartAsync();
    }

    public async Task SendMessageToAllAsync(string? title, string? message, string link, FieldMapperDto fields)
    {
        if (Connection.State == HubConnectionState.Connected)
        {
            var replacements = fields.Fildes();
            var messageBody = message?.ReplacePlaceholders(replacements);
            await Connection.InvokeAsync("SendMessageToAll", title, messageBody, link);
        }
    }

    public async Task SendMessageToUserAsync(string? userId, string? title, string? message, string link, FieldMapperDto fields)
    {
        if (Connection.State == HubConnectionState.Connected)
        {
            var replacements = fields.Fildes();
            var messageBody = message?.ReplacePlaceholders(replacements);
            await Connection.InvokeAsync("SendMessageToUser", userId, title, messageBody, link);
        }
    }

    public async Task SendMessageToGroupAsync(string groupName, string? title, string? message, string link, FieldMapperDto fields)
    {
        if (Connection.State == HubConnectionState.Connected)
        {
            var replacements = fields.Fildes();
            var messageBody = message?.ReplacePlaceholders(replacements);
            await Connection.InvokeAsync("SendMessageToGroup", groupName, title, messageBody, link);
        }
    }

    public async Task AddToGroupAsync(string groupName)
    {
        if (Connection.State == HubConnectionState.Connected)
            await Connection.InvokeAsync("AddToGroup", groupName);
    }

    public async Task RemoveFromGroupAsync(string groupName)
    {
        if (Connection.State == HubConnectionState.Connected)
            await Connection.InvokeAsync("RemoveFromGroup", groupName);
    }

    public async Task StopAsync()
    {
        if (Connection.State == HubConnectionState.Connected)
            await Connection.StopAsync();
    }

    public void Dispose()
    {
        Connection.DisposeAsync().AsTask().Wait();
    }
}
