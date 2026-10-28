using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Options;
using MyHHParser.Core.Configuration;

namespace MyHHParser.Core.DependencyInjection;

public static class ServiceCollectionExtensions
{
	public const string HhHttpClientName = "HhApi";

	public static IServiceCollection AddHhParserCore(this IServiceCollection services, IConfiguration configuration)
	{
		services.Configure<HhApiOptions>(configuration.GetSection(HhApiOptions.SectionName));
		services.Configure<SearchOptions>(configuration.GetSection(SearchOptions.SectionName));
		services.Configure<ParserOptions>(configuration.GetSection(ParserOptions.SectionName));

		services.AddHttpClient(HhHttpClientName, (sp, client) =>
		{
			var options = sp.GetRequiredService<IOptions<HhApiOptions>>().Value;

			client.BaseAddress = new Uri(options.BaseUrl.TrimEnd('/') + "/");
			client.Timeout = TimeSpan.FromSeconds(options.TimeoutSeconds);
			client.DefaultRequestHeaders.TryAddWithoutValidation("User-Agent", options.UserAgent);
			client.DefaultRequestHeaders.Accept.ParseAdd("application/json");
		});

		return services;
	}
}
