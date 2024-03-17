using Microsoft.EntityFrameworkCore;
using TicTacToeBlazor.Data;
using TicTacToeBlazor.Services;
using TicTacToeBlazorServer.Services;

var builder = WebApplication.CreateBuilder(args);

var services = builder.Services;
services.AddControllers();
services.AddEndpointsApiExplorer();
services.AddDbContext<LobbyContext>(options =>
    options.UseSqlServer(builder.Configuration.GetConnectionString("LobbyDB")));
services.AddScoped<ILobbyService, LobbyService>();
services.AddSingleton<IGameLogicCommandService, TicTacToeLogicCommandService>();
services.AddSwaggerGen();

var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
