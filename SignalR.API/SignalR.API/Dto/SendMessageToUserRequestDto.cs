namespace SignalR.API.Dto
{
    public class SendMessageToUserRequestDto
    {
        public string? UserId { get; set; }
        public string? Title { get; set; }
        public string? Message { get; set; }
        public string? Link { get; set; }
    }
}
