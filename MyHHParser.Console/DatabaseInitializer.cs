using Microsoft.Extensions.DependencyInjection;
using MyHHParser.Data;

namespace MyHHParser.Console;

internal static class DatabaseInitializer
{
	public static async Task InitializeAsync(IServiceProvider services)
	{
		using var scope = services.CreateScope();
		var dbContext = scope.ServiceProvider.GetRequiredService<HhParserDbContext>();
		await dbContext.Database.EnsureCreatedAsync();
	}
}
