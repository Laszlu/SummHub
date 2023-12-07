using Microsoft.AspNetCore.Components.Web;
using Microsoft.AspNetCore.Components.WebAssembly.Hosting;
using LanguageDetection;
using SummHub;
using SummHub.Controller;
using SummHub.Model;

var builder = WebAssemblyHostBuilder.CreateDefault(args);
builder.RootComponents.Add<App>("#app");
builder.RootComponents.Add<HeadOutlet>("head::after");

//var client = new HttpClient { BaseAddress = new Uri(builder.HostEnvironment.BaseAddress) };

builder.Services.AddSingleton<HttpClient>();
builder.Services.AddSingleton<LanguageDetector>();
builder.Services.AddSingleton<ArticlesService>();
builder.Services.AddSingleton<NewsApiController>();
builder.Services.AddSingleton<MsTranslatorApiController>();
builder.Services.AddSingleton<ControllerManager>();

//builder.Services.AddScoped(sp => new ControllerManager(client));

await builder.Build().RunAsync();
