// See https://aka.ms/new-console-template for more information
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.DependencyInjection;
using AirlineSendAgent.App;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using AirlineSendAgent.Client;

internal class Program
{
    private static void Main(string[] args)
    {
        var builder = new ConfigurationBuilder()
    .SetBasePath(Directory.GetCurrentDirectory())
    .AddJsonFile("appsettings.json", optional: false, reloadOnChange: true)
    .AddUserSecrets(typeof(Program).Assembly, optional: true)
    .AddEnvironmentVariables()
    .AddCommandLine(args);

        var config = builder.Build();
        var host = Host.CreateDefaultBuilder()
            .ConfigureServices((context, services) =>
            {
                services.AddSingleton<IAppHost, AppHost>();
                services.AddSingleton<IWebhookClient, WebhookClient>();
                services.AddDbContext<AirlineSendAgent.Data.SendAgentDbContext>(opt => opt.UseSqlServer
                        (context.Configuration.GetConnectionString("AirlineConnection")));

                services.AddHttpClient();
            }).Build();

        host.Services.GetService<IAppHost>().Run();
    }
}