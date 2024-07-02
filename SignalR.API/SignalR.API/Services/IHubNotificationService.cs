using SignalR.API.Dto;

namespace SignalR.API.Services;
public interface IHubNotificationService
{
    Task StartAsync();

    Task SendMessageToAllAsync(string? title, string? message, string link, FieldMapperDto fields);

    Task SendMessageToUserAsync(string? userId, string? title, string? message, string link, FieldMapperDto fields);

    Task SendMessageToGroupAsync(string groupName, string? title, string? message, string link, FieldMapperDto fields);

    Task AddToGroupAsync(string groupName);

    Task RemoveFromGroupAsync(string groupName);

    Task StopAsync();
}