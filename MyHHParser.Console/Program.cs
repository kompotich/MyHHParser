using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using MyHHParser.Core.DependencyInjection;

namespace MyHHParser.Console;

public class Program
{
	public static void Main(string[] args)
	{
		var builder = Host.CreateApplicationBuilder(args);

		builder.Configuration.AddJsonFile("config.json", optional: false, reloadOnChange: true);
		builder.Services.AddHhParserCore(builder.Configuration);

		using var host = builder.Build();

		_ = host.Services.GetRequiredService<IHttpClientFactory>();
	}
}
