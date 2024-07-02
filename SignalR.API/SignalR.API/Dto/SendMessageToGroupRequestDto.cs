namespace SignalR.API.Dto
{
    public class SendMessageToGroupRequestDto
    {
        public string? GroupName { get; set; }
        public string? Title { get; set; }
        public string? Message { get; set; }
        public string? Link { get; set; }
    }
}
