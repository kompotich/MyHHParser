using System.Text.Json.Serialization;

namespace MyHHParser.Core.Services.HhApi;

internal sealed class HhSalary
{
	[JsonPropertyName("from")]
	public decimal? From { get; init; }

	[JsonPropertyName("to")]
	public decimal? To { get; init; }

	[JsonPropertyName("currency")]
	public string? Currency { get; init; }

	[JsonPropertyName("gross")]
	public bool? Gross { get; init; }
}
