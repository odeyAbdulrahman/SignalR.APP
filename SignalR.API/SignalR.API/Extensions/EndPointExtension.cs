using SignalR.API.Hubs;

namespace UserManagement.API.Extensions
{
    public static class EndPointExtension
    {
        public static void UseCustomEndPoint(this IApplicationBuilder app)
        {
            app.UseEndpoints(endpoints =>
            {
                endpoints.MapHub<MessageHub>("/notificationHub");
                endpoints.MapControllers();
            });
        }
    }
}
