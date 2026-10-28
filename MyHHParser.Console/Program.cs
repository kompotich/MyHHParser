using Microsoft.Extensions.DependencyInjection;
using MyHHParser.Core.Services;
using MyHHParser.Data.Repositories;

namespace MyHHParser.Console;

public class Program
{
	public static async Task Main(string[] args)
	{
		using var host = ConsoleHostFactory.Create(args);
		await DatabaseInitializer.InitializeAsync(host.Services);

		using var scope = host.Services.CreateScope();
		var vacancySearchService = scope.ServiceProvider.GetRequiredService<IVacancySearchService>();
		var vacancies = await vacancySearchService.GetVacanciesAsync();
		var vacancyRepository = scope.ServiceProvider.GetRequiredService<IVacancyRepository>();
		await vacancyRepository.SaveAsync(vacancies);
		VacancyConsolePrinter.Print(vacancies);
	}
}
