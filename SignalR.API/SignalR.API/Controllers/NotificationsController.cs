using Core.Helper.Commen.Dtos;
using Microsoft.AspNetCore.Mvc;
using Core.Helper.Implementations;
using NotificationManagement.API.Dto;
using UserManagement.API.Controllers;
using Microsoft.AspNetCore.Authorization;
using NotificationManagement.API.Services;

namespace NotificationManagement.API.Controllers
{
    [Authorize(policy: "ClientIdPolicy", Roles = "Admin,Client")]

    public class NotificationsController : BaseController
    {
        private readonly INotificationService NotificationService;
        private readonly IHubNotificationService HubNotificationService;


        public NotificationsController(INotificationService notificationService, IConfiguration configuration, IHubNotificationService hubNotificationService)
            : base(configuration)
        {
            NotificationService = notificationService;
            HubNotificationService = hubNotificationService;
        }

        [HttpPost("List")]
        public async Task<ActionResult<ResponseViewModel<IList<NotifcationDto>?>>> GetList()
        {
            return Ok(await NotificationService.GetListAsync(CurrentUser()));
        }

        [HttpPost("Start")]
        public async Task<ActionResult> Start()
        {
            await HubNotificationService.StartAsync();
            return Ok();
        }

        [HttpPost("Stop")]
        public async Task<ActionResult> Stop()
        {
            await HubNotificationService.StopAsync();
            return Ok();
        }

        [HttpPost("SendToAll")]
        public async Task<ActionResult> SendMessageToAll([FromBody] SendMessageToAllRequestDto request)
        {
            await HubNotificationService.SendMessageToAllAsync(
                request.Title, 
                request.Message, 
                request.Link ?? string.Empty, 
                new FieldMapperDto() 
                );
            return Ok();
        }

        [HttpPost("SendToUser")]
        public async Task<ActionResult> SendMessageToUser([FromBody] SendMessageToUserRequestDto request)
        {
            await HubNotificationService.SendMessageToUserAsync(
                request.UserId ?? string.Empty, 
                request.Title, 
                request.Message,
                request.Link ?? string.Empty,
                new FieldMapperDto());
            return Ok();
        }

        [HttpPost("SendToGroup")]
        public async Task<ActionResult> SendMessageToGroup([FromBody] SendMessageToGroupRequestDto request)
        {
            await HubNotificationService.SendMessageToGroupAsync(
                request.GroupName ?? string.Empty,
                request.Title,
                request.Message ?? string.Empty,
                request.Link ?? string.Empty,
                new FieldMapperDto());
            return Ok();
        }
    }
}
