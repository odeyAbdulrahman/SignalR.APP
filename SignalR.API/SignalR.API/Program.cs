using UserManagement.API.Extensions;

var builder = WebApplication.CreateBuilder(args);
builder.ConfigureAppConfiguration();
builder.Services.AddServies(
    configuration: builder.Configuration, 
    notificationHubUrl: builder.Configuration
    .GetSection(key: "NotificationHub")[key: "BaseUrl"]);

var app = builder.Build();
if (app.Environment.IsStaging())
{
}
else
{
}

app.UseCors("AllowOrigin");
app.UseDeveloperExceptionPage();
app.UseRouting();
app.UseSwagger();
app.UseSwaggerUI(); 
app?.Run();