using Microsoft.AspNetCore.Components.Web;
using Microsoft.AspNetCore.Components.WebAssembly.Hosting;
using Oogarts.Shared.EyeConditions;
using Oogarts.Client.EyeConditions;
using Client;
using Shared.Articles;
using Client.Articles;
using Radzen;
using Client.Files;
using Client.Symptoms;

var builder = WebAssemblyHostBuilder.CreateDefault(args);
builder.RootComponents.Add<App>("#app");
builder.RootComponents.Add<HeadOutlet>("head::after");

builder.Services.AddRadzenComponents();
//builder.Services.AddAuthorizationCore();
//builder.Services.AddSingleton<FakeAuthenticationProvider>();
//builder.Services.AddScoped<AuthenticationStateProvider>(provider => provider.GetRequiredService<FakeAuthenticationProvider>());
//builder.Services.AddTransient<FakeAuthorizationMessageHandler>();

builder.Services.AddHttpClient("Project.ServerAPI", client => client.BaseAddress = new Uri(builder.HostEnvironment.BaseAddress));
				//.AddHttpMessageHandler<FakeAuthorizationMessageHandler>();

builder.Services.AddScoped(sp => sp.GetRequiredService<IHttpClientFactory>().CreateClient("Project.ServerAPI"));


builder.Services.AddScoped<IEyeConditionService, EyeConditionService>();
builder.Services.AddScoped<ISymptomService, SymptomService>();
builder.Services.AddScoped<IArticleService, ArticleService>();
builder.Services.AddHttpClient<IStorageService, AzureBlobStorageService>();

await builder.Build().RunAsync();

