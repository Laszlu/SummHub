using Microsoft.AspNetCore.Components.Web;
using Microsoft.AspNetCore.Components.WebAssembly.Hosting;
using LanguageDetection;
using MudBlazor.Services;
using SummHub;
using SummHub.Controller;
using SummHub.Model;

var builder = WebAssemblyHostBuilder.CreateDefault(args);
builder.RootComponents.Add<App>("#app");
builder.RootComponents.Add<HeadOutlet>("head::after");

builder.Services.AddSingleton<HttpClient>();
builder.Services.AddSingleton<LanguageDetector>();
builder.Services.AddSingleton<ArticlesService>();
builder.Services.AddSingleton<NewsApiController>();
builder.Services.AddSingleton<MsTranslatorApiController>();
builder.Services.AddSingleton<ControllerManager>();
builder.Services.AddSingleton<SearchService>();

builder.Services.AddMudServices();

await builder.Build().RunAsync();
