using CourseraApp.Client.Services;
using Microsoft.AspNetCore.Components.WebAssembly.Hosting;

namespace CourseraApp.Client;

class Program
{
    static async Task Main(string[] args)
    {
        var builder = WebAssemblyHostBuilder.CreateDefault(args);

        builder.Services.AddSingleton<DataService>();
        builder.Services.AddScoped<FeedbackService>();

        await builder.Build().RunAsync();
    }
}
