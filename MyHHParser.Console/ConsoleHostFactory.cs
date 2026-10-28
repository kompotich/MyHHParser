using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Hosting;
using MyHHParser.Core.DependencyInjection;
using MyHHParser.Data.DependencyInjection;

namespace MyHHParser.Console;

internal static class ConsoleHostFactory
{
	public static IHost Create(string[] args)
	{
		var builder = Host.CreateApplicationBuilder(args);
		builder.Configuration.AddJsonFile("config.json", optional: false, reloadOnChange: true);
		builder.Services.AddHhParserCore(builder.Configuration);
		builder.Services.AddHhParserData(builder.Configuration);

		return builder.Build();
	}
}
