using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Options;
using MyHHParser.Data.Configuration;
using MyHHParser.Data.Repositories;

namespace MyHHParser.Data.DependencyInjection;

public static class ServiceCollectionExtensions
{
	public static IServiceCollection AddHhParserData(this IServiceCollection services, IConfiguration configuration)
	{
		services.Configure<DatabaseOptions>(configuration.GetSection(DatabaseOptions.SectionName));

		services.AddDbContext<HhParserDbContext>((sp, options) =>
		{
			var databaseOptions = sp.GetRequiredService<IOptions<DatabaseOptions>>().Value;
			options.UseSqlite(databaseOptions.ConnectionString);
		});
		services.AddScoped<IVacancyRepository, VacancyRepository>();

		return services;
	}
}
