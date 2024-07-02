using SignalR.API.Dto;

namespace SignalR.API.Services
{
    public interface INotificationService
    {
        Task PutListAsync(IList<NotifcationDto>? models);
        Task<IList<NotifcationDto>?> GetListAsync(string userId);
        Task<IList<NotifcationDto>?> GetListAsync(string userId, NotifcationDto newNotifcation);
    }
}
