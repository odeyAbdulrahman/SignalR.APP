
using SignalR.API.Hubs;
using SignalR.API.Services;
using Microsoft.OpenApi.Models;

namespace UserManagement.API.Extensions
{
    public static class IocContainerExtension
    {
        public static void AddServies(this IServiceCollection services, IConfiguration configuration, string notificationHubUrl)
        {
            // Add services to the container.
            services.AddControllers();
            services.AddEndpointsApiExplorer();
            //Add SignalR service
            services.AddSignalR().AddHubOptions<MessageHub>(options =>
            {
                options.EnableDetailedErrors = true;
            });

            //Add Swagger Gen 1
            services.AddSwaggerGen(x =>
            {
                x.SwaggerDoc("Swagger-doc", new OpenApiInfo
                {
                    Title = "test",
                    Version = "V1",
                    Description = "test",
                    Contact = new OpenApiContact
                    {
                        Name = "test",
                        Email = "test",
                    },
                });
            });

            services.AddHttpClient();
            services.AddHttpContextAccessor();
            services.AddCors(options =>
            {
                options.AddPolicy(
                name: "AllowOrigin",
                builder =>
                {
                    builder.AllowAnyOrigin()
                            .AllowAnyMethod()
                            .AllowAnyHeader();
                });
            });

            services.AddSingleton<IHubNotificationService, HubNotificationService>(sp =>
                new HubNotificationService(notificationHubUrl ?? string.Empty));
        }
    }
}
