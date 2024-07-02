using FileManager.Helper.Utilities;

namespace NotificationManagement.API.Dto
{
    public class NotifcationDto
    {
        public string? Id { get; set; } = Guid.NewGuid().ToString();
        public string? UserId { get; set; }
        public string? GroupName { get; set; }

        public string? Title { get; set; }
        public string? Message { get; set; }
        public string? Link { get; set; }
        public DateTime Timestamp { get; set; } = CoreUtility.GetCurrentDateTime();
    }
}
