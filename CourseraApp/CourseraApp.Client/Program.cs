using CourseraApp.Client.Models;
using CourseraApp.Client.Services;
using FluentValidation;
using Microsoft.AspNetCore.Components.Web;
using Microsoft.AspNetCore.Components.WebAssembly.Hosting;
namespace CourseraApp.Client;

class Program
{
    static async Task Main(string[] args)
    {
        var builder = WebAssemblyHostBuilder.CreateDefault(args);
        builder.RootComponents.Add<App>("#app");
        builder.RootComponents.Add<HeadOutlet>("head::after");


        builder.Services.AddScoped<IValidator<Feedback>, FeedbackValidator>();

        builder.Services.AddSingleton<DataService>();
        builder.Services.AddScoped<FeedbackService>();
        builder.Services.AddSingleton<FeedbackInMemoryService>();

        await builder.Build().RunAsync();
    }
}
