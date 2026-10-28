using System.Text.Json.Serialization;

namespace MyHHParser.Core.Services.HhApi;

internal sealed class HhNamedItem
{
	[JsonPropertyName("name")]
	public string? Name { get; init; }
}
