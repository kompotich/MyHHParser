using MyHHParser.Core.DependencyInjection;
using MyHHParser.Data;
using MyHHParser.Data.DependencyInjection;

var builder = WebApplication.CreateBuilder(args);

builder.Configuration.AddJsonFile("config.json", optional: false, reloadOnChange: true);
builder.Services.AddHhParserCore(builder.Configuration);
builder.Services.AddHhParserData(builder.Configuration);
builder.Services.AddControllers();

var app = builder.Build();

using (var scope = app.Services.CreateScope())
{
	var dbContext = scope.ServiceProvider.GetRequiredService<HhParserDbContext>();
	await dbContext.Database.EnsureCreatedAsync();
}

app.MapControllers();
app.Run();
