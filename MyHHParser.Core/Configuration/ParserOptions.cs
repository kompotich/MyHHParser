namespace MyHHParser.Core.Configuration;

public sealed class ParserOptions
{
	public const string SectionName = "Parser";

	public int DelayMs { get; set; } = 500;
	public string OutputPath { get; set; } = "vacancies.json";
}
