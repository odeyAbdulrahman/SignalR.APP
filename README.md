# SignalR.APP

This project consists of two applications: a server and a client. The server is written in .NET Core, and the client is written in ReactJS. The server sends real-time notifications to the client and stores these notifications in Redis on the server side.

## Project Components

1. **Server:**
    - Written in .NET Core.
    - Responsible for sending real-time notifications.
    - Stores notifications in Redis.
    - Uses a library like SignalR to manage real-time connections.

2. **Client:**
    - Written in ReactJS.
    - Receives real-time notifications from the server.
    - Displays notifications to users instantly.

## Detailed Steps

### Setting Up the Server

1. **Create a .NET Core Project:**
    ```bash
    dotnet new webapi -n SignalR.API
    ```

2. **Set Up SignalR:**
    ```bash
    dotnet add package Microsoft.AspNetCore.SignalR
    ```

    Configure SignalR in `Startup.cs`:
    ```csharp
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
    ```

3. **Create a Notification Hub:**
    Create a new file named `NotificationHub.cs` and add the following:
    ```csharp
    public class MessageHub : Hub
    {
        public MessageHub()
        {
        }

        public async Task SendMessageToAll(string title, string message, string link)
        {
            await Clients.All.SendAsync("ReceiveMessage", title, message, link);
        }

        public async Task SendMessageToUser(string userId, string title, string message, string link)
        {
            await Clients.User(userId).SendAsync("ReceiveMessage", title, message, link);
        }

        public async Task SendMessageToGroup(string groupName, string title, string message, string link)
        {
            await Clients.Group(groupName).SendAsync("ReceiveMessage", title, message, link);
        }
    }
    ```

4. **Set Up Redis:**
    ```bash
    dotnet add package StackExchange.Redis
    ```

    Configure the Redis connection in `Startup.cs` and add the necessary services.

### Setting Up the Client

1. **Create a React Project:**
    ```bash
    npx create-react-app signalr-react-app
    cd signalr-react-app
    npm install @microsoft/signalr
    ```

2. **Connect to SignalR:**
    Create a new file for SignalR connection such as `signalrService.js`:
    ```javascript
    import * as signalR from '@microsoft/signalr';

    let connection;

    export const startConnection = async (hubUrl, authToken) => {
        connection = new signalR.HubConnectionBuilder()
            .withUrl(hubUrl, {
                accessTokenFactory: () => authToken,
                withCredentials: false
            })
            .withAutomaticReconnect()
            .configureLogging(signalR.LogLevel.Information)
            .build();

        connection.onreconnecting((error) => {
            console.assert(connection.state === signalR.HubConnectionState.Reconnecting);
            console.log("Connection lost due to error. Reconnecting.", error);
        });

        connection.onreconnected((connectionId) => {
            console.assert(connection.state === signalR.HubConnectionState.Connected);
            console.log("Connection reestablished. Connected with connectionId", connectionId);
        });

        try {
            await connection.start();
            console.assert(connection.state === signalR.HubConnectionState.Connected);
            console.log("Connected to SignalR hub");
        } catch (err) {
            console.assert(connection.state === signalR.HubConnectionState.Disconnected);
            console.error("SignalR Connection Error: ", err);
            setTimeout(() => startConnection(hubUrl, authToken), 5000);  // Try to reconnect after 5 seconds.
        }
    };
    export const addReceiveMessageListener = (callback) => {
        if (connection) {
            connection.on('ReceiveMessage', (title, message, link) => {
                    callback(title, message, link);
            });
        } else {
            console.error("Connection has not been established yet.");
        }
    };

    export const addNotificationListenerByConnectionId = (callback) => {
        if (connection) {
            connection.on('ReceiveMessage', (message, connectionId) => {
                if (connection.connectionId === connectionId) {
                    callback(message);
                }
            });
        } else {
            console.error("Connection has not been established yet.");
        }
    };

    export const stopConnection = async () => {
        if (connection) {
            await connection.stop();
        }
    };

    ```

## Additional Notes

- Ensure Redis Server is running on your machine or use a managed Redis service.
- Set up CORS on the server to allow requests from the client.
- Handle errors and exceptions on both sides to improve application stability.

With these steps, you can create a real-time notification system using .NET Core and ReactJS and store notifications in Redis. If you have any questions or need further assistance, feel free to ask.
