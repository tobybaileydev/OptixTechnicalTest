var builder = WebAssemblyHostBuilder.CreateDefault(args);

builder.Services.AddScoped(sp => new HttpClient { BaseAddress = new Uri(builder.HostEnvironment.BaseAddress) });
builder.Services.AddMudServices();
builder.Services.AddScoped<IValidationHelper, ValidationHelper>();
builder.Services.AddScoped<IHomePageService, HomePageService>();
builder.Services.AddScoped<IHttpHelper, HttpHelper>();
builder.Services.AddScoped<IApiRequestService, ApiRequestService>();
await builder.Build().RunAsync();
