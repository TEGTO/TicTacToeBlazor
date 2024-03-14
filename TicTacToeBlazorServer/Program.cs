using Microsoft.AspNetCore.ResponseCompression;
using Microsoft.EntityFrameworkCore;
using TicTacToeBlazorServer.Components;
using TicTacToeBlazorServer.Data;
using TicTacToeBlazorServer.Hubs;
using TicTacToeBlazorServer.Middlewares;
using TicTacToeBlazorServer.Services;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddRazorComponents()
    .AddInteractiveServerComponents();

var services = builder.Services;

services.AddResponseCompression(opts =>
{
    opts.MimeTypes = ResponseCompressionDefaults.MimeTypes.Concat(
          new[] { "application/octet-stream" });
});
services.AddDbContext<LobbyContext>(options =>
    options.UseSqlServer(builder.Configuration.GetConnectionString("LobbyDB")));
services.AddScoped<ILobbyService, LobbyService>();
services.AddScoped<RedirectManager>();
services.AddScoped<ITimerService, TimerService>();
var app = builder.Build();
// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Error", createScopeForErrors: true);
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}

app.UseHttpsRedirection();
app.UseMiddleware<UniqueIdMiddleware>();

app.UseStaticFiles();
app.UseAntiforgery();


app.MapRazorComponents<App>()
    .AddInteractiveServerRenderMode();

app.MapHub<LobbyHub>("/lobbyhub");

app.Run();
