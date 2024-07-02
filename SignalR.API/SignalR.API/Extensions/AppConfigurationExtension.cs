namespace UserManagement.API.Extensions
{
    public static class AppConfigurationExtension
    {
        public static IHostBuilder ConfigureAppConfiguration(this WebApplicationBuilder builder)
        {
            return builder.Host.ConfigureAppConfiguration((hostingContext, config) =>
            {
                config.SetBasePath(hostingContext.HostingEnvironment.ContentRootPath);
                config.AddJsonFile($"Appsettings/appsettings.json", true, true);
                config.AddJsonFile($"Appsettings/appsettings.{hostingContext.HostingEnvironment.EnvironmentName}.json", true, true);
                config.AddJsonFile($"Appsettings/Lang/applang.ar.json", true, true);
                config.AddJsonFile($"Appsettings/Lang/applang.en.json", true, true);
                config.AddEnvironmentVariables();
            });
        }
    }
}
