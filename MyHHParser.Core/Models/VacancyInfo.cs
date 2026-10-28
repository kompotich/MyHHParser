namespace MyHHParser.Core.Models;

public sealed class VacancyInfo
{
	public string ExternalId { get; init; } = string.Empty;
	public string Name { get; init; } = string.Empty;
	public string? EmployerName { get; init; }
	public string? AreaName { get; init; }
	public string? Url { get; init; }
	public decimal? SalaryFrom { get; init; }
	public decimal? SalaryTo { get; init; }
	public string? SalaryCurrency { get; init; }
	public bool? SalaryGross { get; init; }
	public string? Experience { get; init; }
	public string? Employment { get; init; }
	public string? Schedule { get; init; }
	public string? Description { get; init; }
	public DateTimeOffset? PublishedAt { get; init; }
}
