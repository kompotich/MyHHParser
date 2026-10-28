namespace MyHHParser.Core.Configuration;

public sealed class SearchOptions
{
	public const string SectionName = "Search";

	public string Text { get; set; } = string.Empty;
	public int Area { get; set; }
	public int PerPage { get; set; } = 50;
	public int Pages { get; set; } = 1;
	public bool OnlyWithSalary { get; set; }
}
