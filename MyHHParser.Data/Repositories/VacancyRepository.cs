using Microsoft.EntityFrameworkCore;
using MyHHParser.Core.Models;
using MyHHParser.Data.Entities;

namespace MyHHParser.Data.Repositories;

public sealed class VacancyRepository : IVacancyRepository
{
	private readonly HhParserDbContext _dbContext;

	public VacancyRepository(HhParserDbContext dbContext)
	{
		_dbContext = dbContext;
	}

	public async Task SaveAsync(
		IReadOnlyCollection<VacancyInfo> vacancies,
		CancellationToken cancellationToken = default)
	{
		if (vacancies.Count == 0)
		{
			return;
		}

		if (vacancies.Any(vacancy => string.IsNullOrWhiteSpace(vacancy.ExternalId)))
		{
			throw new InvalidDataException("A vacancy cannot be saved without an external ID.");
		}

		var uniqueVacancies = vacancies
			.GroupBy(vacancy => vacancy.ExternalId, StringComparer.Ordinal)
			.Select(group => group.Last())
			.ToArray();
		var externalIds = uniqueVacancies.Select(vacancy => vacancy.ExternalId).ToArray();
		var existingVacancies = await _dbContext.Vacancies
			.Where(vacancy => externalIds.Contains(vacancy.ExternalId))
			.ToDictionaryAsync(vacancy => vacancy.ExternalId, cancellationToken);
		var parsedAt = DateTimeOffset.UtcNow;

		foreach (var vacancyInfo in uniqueVacancies)
		{
			if (!existingVacancies.TryGetValue(vacancyInfo.ExternalId, out var vacancy))
			{
				vacancy = new Vacancy
				{
					ExternalId = vacancyInfo.ExternalId
				};
				_dbContext.Vacancies.Add(vacancy);
			}

			vacancy.Name = vacancyInfo.Name;
			vacancy.EmployerName = vacancyInfo.EmployerName;
			vacancy.AreaName = vacancyInfo.AreaName;
			vacancy.Url = vacancyInfo.Url;
			vacancy.SalaryFrom = vacancyInfo.SalaryFrom;
			vacancy.SalaryTo = vacancyInfo.SalaryTo;
			vacancy.SalaryCurrency = vacancyInfo.SalaryCurrency;
			vacancy.SalaryGross = vacancyInfo.SalaryGross;
			vacancy.Experience = vacancyInfo.Experience;
			vacancy.Employment = vacancyInfo.Employment;
			vacancy.Schedule = vacancyInfo.Schedule;
			vacancy.Description = vacancyInfo.Description;
			vacancy.PublishedAt = vacancyInfo.PublishedAt;
			vacancy.ParsedAt = parsedAt;
		}

		await _dbContext.SaveChangesAsync(cancellationToken);
	}
}
