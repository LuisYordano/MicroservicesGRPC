using BlazorFrontend.Components;
using Grpc.Net.Client.Web;
using GrpcBackend;

var builder = WebApplication.CreateBuilder(args);

builder.AddServiceDefaults();

// Add services to the container.
builder.Services.AddRazorComponents()
    .AddInteractiveServerComponents();

builder.Services
    .AddGrpcClient<WeatherForecasts.WeatherForecastsClient>(options =>
    {
        options.Address = new Uri("http://grpcbackend");
    })
    .ConfigurePrimaryHttpMessageHandler(
        () => new GrpcWebHandler(new HttpClientHandler()));

var app = builder.Build();

app.MapDefaultEndpoints();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Error", createScopeForErrors: true);
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}

app.UseHttpsRedirection();

app.UseStaticFiles();
app.UseAntiforgery();

app.MapRazorComponents<App>()
    .AddInteractiveServerRenderMode();

app.Run();
