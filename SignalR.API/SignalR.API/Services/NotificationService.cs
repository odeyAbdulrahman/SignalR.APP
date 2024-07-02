using SignalR.API.Dto;
using System.Text.Json;

namespace SignalR.API.Services
{
    public class NotificationService : INotificationService
    {
        private const int Take = 50;
        private readonly IRedisCache RedisCache;
        private const string GlobalKey = "notification:";

        public NotificationService(IRedisCache redisCache)
        {
            RedisCache = redisCache;
        }
        public async Task<IList<NotifcationDto>?> GetListAsync(string userId, NotifcationDto newNotifcation)
        {
            var key = $"{GlobalKey}{userId}";
            var content = await RedisCache.GetAsync(key);
            var list = JsonSerializer.Deserialize<IList<NotifcationDto>>(content) ?? new List<NotifcationDto>();
            list?.Add(newNotifcation);
            return list?.OrderByDescending(x => x.Timestamp).Take(Take).ToList();
        }

        public async Task<IList<NotifcationDto>?> GetListAsync(string userId)
        {
            var key = $"{GlobalKey}{userId}";
            var content = await RedisCache.GetAsync(key);
            if (string.IsNullOrEmpty(content))
                return new List<NotifcationDto>();

            var list = JsonSerializer.Deserialize<IList<NotifcationDto>>(content);
            return new List<NotifcationDto>(list?.OrderByDescending(x => x.Timestamp).Take(Take).ToList()
                ?? new List<NotifcationDto>());
        }

        public async Task PutListAsync(IList<NotifcationDto>? models)
        {
            var key = $"{GlobalKey}{models?.FirstOrDefault()?.UserId}";
            var content = JsonSerializer.Serialize(models);
            await RedisCache.UpdateAsync(key, content);
        }
    }
}
