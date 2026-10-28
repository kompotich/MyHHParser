namespace MyHHParser.Data.Entities;

public sealed class Vacancy
{
	public int Id { get; set; }
	public string ExternalId { get; set; } = string.Empty;
	public string Name { get; set; } = string.Empty;
	public string? EmployerName { get; set; }
	public string? AreaName { get; set; }
	public string? Url { get; set; }
	public decimal? SalaryFrom { get; set; }
	public decimal? SalaryTo { get; set; }
	public string? SalaryCurrency { get; set; }
	public bool? SalaryGross { get; set; }
	public string? Experience { get; set; }
	public string? Employment { get; set; }
	public string? Schedule { get; set; }
	public string? Description { get; set; }
	public DateTimeOffset? PublishedAt { get; set; }
	public DateTimeOffset ParsedAt { get; set; }
}
