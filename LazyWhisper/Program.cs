using System;
using System.Threading.Tasks;
using Microsoft.Extensions.Hosting;
using ConsoleAppFramework;
using LazyWhisper.Module;
using Microsoft.Extensions.DependencyInjection;

namespace LazyWhisper
{
    class Program
    {
        static async Task Main(string[] args)
        {
            await Host.CreateDefaultBuilder().ConfigureServices((hostContext, services) =>
            {
                services.AddOptions();
                services.AddSingleton<CustomCommandModule>();
                services.Configure<Config>(hostContext.Configuration.GetSection("Config"));
            }).RunConsoleAppFrameworkAsync<LazyWhisper>(args);
        }
    }
}
