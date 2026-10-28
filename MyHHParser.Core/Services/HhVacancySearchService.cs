using System.Text.Json;
using Microsoft.Extensions.Options;
using MyHHParser.Core.Configuration;
using MyHHParser.Core.DependencyInjection;
using MyHHParser.Core.Models;
using MyHHParser.Core.Services.HhApi;

namespace MyHHParser.Core.Services;

public sealed class HhVacancySearchService : IVacancySearchService
{
	private readonly HttpClient _httpClient;
	private readonly HhApiOptions _hhApiOptions;
	private readonly SearchOptions _searchOptions;
	private readonly ParserOptions _parserOptions;

	public HhVacancySearchService(
		IHttpClientFactory httpClientFactory,
		IOptions<HhApiOptions> hhApiOptions,
		IOptions<SearchOptions> searchOptions,
		IOptions<ParserOptions> parserOptions)
	{
		_httpClient = httpClientFactory.CreateClient(ServiceCollectionExtensions.HhHttpClientName);
		_hhApiOptions = hhApiOptions.Value;
		_searchOptions = searchOptions.Value;
		_parserOptions = parserOptions.Value;
	}

	public async Task<IReadOnlyList<VacancyInfo>> GetVacanciesAsync(CancellationToken cancellationToken = default)
	{
		var vacancies = new List<VacancyInfo>();

		for (var page = 0; page < _searchOptions.Pages; page++)
		{
			if (page > 0 && _parserOptions.DelayMs > 0)
			{
				await Task.Delay(_parserOptions.DelayMs, cancellationToken);
			}

			var uri = MakeSearchUri(page);

			using var response = await _httpClient.GetAsync(uri, cancellationToken);
			response.EnsureSuccessStatusCode();

			await using var contentStream = await response.Content.ReadAsStreamAsync(cancellationToken);
			var searchResponse = await JsonSerializer.DeserializeAsync<HhVacancySearchResponse>(
				contentStream,
				cancellationToken: cancellationToken);

			if (searchResponse?.Items is null || searchResponse.Items.Length == 0)
			{
				break;
			}

			vacancies.AddRange(searchResponse.Items.Select(MapVacancy));
		}

		return vacancies;
	}

	private string MakeSearchUri(int page)
	{
		var query = new List<string>
		{
			$"area={_searchOptions.Area}",
			$"per_page={_searchOptions.PerPage}",
			$"page={page}"
		};

		if (!string.IsNullOrWhiteSpace(_searchOptions.Text))
		{
			query.Add($"text={Uri.EscapeDataString(_searchOptions.Text)}");
		}

		if (_searchOptions.OnlyWithSalary)
		{
			query.Add("only_with_salary=true");
		}

		return $"{_hhApiOptions.BaseUrl}/vacancies?{string.Join("&", query)}";
	}

	private static VacancyInfo MapVacancy(HhVacancy vacancy)
	{
		return new VacancyInfo
		{
			ExternalId = vacancy.Id ?? string.Empty,
			Name = vacancy.Name ?? string.Empty,
			EmployerName = vacancy.Employer?.Name,
			AreaName = vacancy.Area?.Name,
			Url = vacancy.Url,
			SalaryFrom = vacancy.Salary?.From,
			SalaryTo = vacancy.Salary?.To,
			SalaryCurrency = vacancy.Salary?.Currency,
			SalaryGross = vacancy.Salary?.Gross,
			Experience = vacancy.Experience?.Name,
			Employment = vacancy.Employment?.Name,
			Schedule = vacancy.Schedule?.Name,
			Description = vacancy.Description,
			PublishedAt = vacancy.PublishedAt
		};
	}
}
