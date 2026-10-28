using System.Text.Json.Serialization;

namespace MyHHParser.Core.Services.HhApi;

internal sealed class HhVacancySearchResponse
{
	[JsonPropertyName("items")]
	public HhVacancy[]? Items { get; init; }
}
