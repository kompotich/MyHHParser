using System.Text.Json.Serialization;

namespace MyHHParser.Core.Services.HhApi;

internal sealed class HhVacancy
{
	[JsonPropertyName("id")]
	public string? Id { get; init; }

	[JsonPropertyName("name")]
	public string? Name { get; init; }

	[JsonPropertyName("employer")]
	public HhNamedItem? Employer { get; init; }

	[JsonPropertyName("area")]
	public HhNamedItem? Area { get; init; }

	[JsonPropertyName("url")]
	public string? Url { get; init; }

	[JsonPropertyName("salary")]
	public HhSalary? Salary { get; init; }

	[JsonPropertyName("experience")]
	public HhNamedItem? Experience { get; init; }

	[JsonPropertyName("employment")]
	public HhNamedItem? Employment { get; init; }

	[JsonPropertyName("schedule")]
	public HhNamedItem? Schedule { get; init; }

	[JsonPropertyName("description")]
	public string? Description { get; init; }

	[JsonPropertyName("published_at")]
	public DateTimeOffset? PublishedAt { get; init; }
}
