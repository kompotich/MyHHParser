namespace MyHHParser.Core.Configuration;

public sealed class HhApiOptions
{
	public const string SectionName = "HhApi";

	public string BaseUrl { get; set; } = "https://api.hh.ru";
	public string UserAgent { get; set; } = "MyHHParser/1.0 (contact@example.com)";
	public int TimeoutSeconds { get; set; } = 30;
}
