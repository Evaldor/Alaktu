using AlaktuManager.Client;
using AlaktuManager.Client.Services;
using Microsoft.AspNetCore.Components.Web;
using Microsoft.AspNetCore.Components.WebAssembly.Hosting;
using AlaktuManager.Shared;
using Radzen;

var builder = WebAssemblyHostBuilder.CreateDefault(args);
builder.RootComponents.Add<App>("#app");
builder.RootComponents.Add<HeadOutlet>("head::after");

builder.Services.AddScoped<ContextMenuService>();
builder.Services.AddScoped<DialogService>();

builder.Services.AddHttpClient<IService<Pipline>, PiplineService>(client =>
{
    client.BaseAddress = new Uri(builder.HostEnvironment.BaseAddress);
    
});
builder.Services.AddHttpClient<IService<SourceType>, SourceTypeService>(client =>
{
    client.BaseAddress = new Uri(builder.HostEnvironment.BaseAddress);
});

await builder.Build().RunAsync();
