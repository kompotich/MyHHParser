using MyHHParser.Core.Models;

namespace MyHHParser.Core.Services;

public interface IVacancySearchService
{
	Task<IReadOnlyList<VacancyInfo>> GetVacanciesAsync(CancellationToken cancellationToken = default);
}
